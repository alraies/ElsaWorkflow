using System.IO;
using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Elsa.Services;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MERP.ElsaService.EntityFrameworkCore;
using MERP.ElsaService.Localization;
using MERP.ElsaService.MultiTenancy;
using MERP.ElsaService.Web.Menus;
using MERP.ElsaService.Web.Workflows;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace MERP.ElsaService.Web;

[DependsOn(
    typeof(ElsaServiceHttpApiModule),
    typeof(ElsaServiceApplicationModule),
    typeof(ElsaServiceEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpSettingManagementWebModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpBackgroundJobsHangfireModule)
)]
public class ElsaServiceWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(ElsaServiceResource),
                typeof(ElsaServiceDomainModule).Assembly,
                typeof(ElsaServiceDomainSharedModule).Assembly,
                typeof(ElsaServiceApplicationModule).Assembly,
                typeof(ElsaServiceApplicationContractsModule).Assembly,
                typeof(ElsaServiceWebModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("ElsaService");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureSwaggerServices(context.Services);
        ConfigureElsa(context, configuration);
        ConfigureHangfire(context, configuration);
    }
    private void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(configuration.GetConnectionString("Default"));
        });
    }
    private void ConfigureElsa(ServiceConfigurationContext context, IConfiguration configuration)
    {
        var elsaSection = configuration.GetSection("Elsa");
        context.Services.AddElsa(options =>
        {
            options.UseEntityFrameworkPersistence(ef =>
                    DbContextOptionsBuilderExtensions
                        .UseSqlServer(ef, configuration.GetConnectionString("Default")))
                .AddConsoleActivities()
                .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                .AddQuartzTemporalActivities()
                .AddJavaScriptActivities()
                .AddWorkflowsFrom<Startup>();
            options.AddConsoleActivities()
                .AddHttpActivities() //add this line to be able to use the http activities
                .AddWorkflow<HelloWorldHttp>(); //workflow that
        });

        context.Services.AddElsaApiEndpoints();
        context.Services.Configure<ApiVersioningOptions>(options => { options.UseApiBehavior = false; });
        context.Services.AddCors(cors => cors.AddDefaultPolicy(policy =>
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .WithExposedHeaders("Content-Disposition")));

        //Disable antiforgery validation for elsa
        Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidateFilter = type =>
                type.Assembly != typeof(Elsa.Server.Api.Endpoints.WorkflowRegistry.Get).Assembly;
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults
            .AuthenticationScheme);
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options => { options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"]; });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle => { bundle.AddFiles("/global-styles.css"); }
            );
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<ElsaServiceWebModule>(); });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<ElsaServiceDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}MERP.ElsaService.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<ElsaServiceDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}MERP.ElsaService.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<ElsaServiceApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}MERP.ElsaService.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<ElsaServiceApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}MERP.ElsaService.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<ElsaServiceWebModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options => { options.MenuContributors.Add(new ElsaServiceMenuContributor()); });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(ElsaServiceApplicationModule).Assembly);
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ElsaService API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "ElsaService API"); });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseHttpActivities();
        app.UseHangfireDashboard();
        app.UseConfiguredEndpoints();

        var workflowRunner = context.ServiceProvider.GetRequiredService<IBuildsAndStartsWorkflow>();
        workflowRunner.BuildAndStartWorkflowAsync<HelloWorldHttp>();
    }
}