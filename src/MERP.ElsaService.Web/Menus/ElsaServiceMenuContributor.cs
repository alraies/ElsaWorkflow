using System.Threading.Tasks;
using MERP.ElsaService.Localization;
using MERP.ElsaService.MultiTenancy;
using MERP.ElsaService.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace MERP.ElsaService.Web.Menus;

public class ElsaServiceMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<ElsaServiceResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                ElsaServiceMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
//add Workflow menu-item
context.Menu.Items.Insert(
    1,
    new ApplicationMenuItem(
        ElsaServiceMenus.Home,
        "Workflow",
        "~/elsa",
        icon:"fas fa-code-branch",
        order:1,
        requiredPermissionName:ElsaServicePermissions.ElsaDashboard));
        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
