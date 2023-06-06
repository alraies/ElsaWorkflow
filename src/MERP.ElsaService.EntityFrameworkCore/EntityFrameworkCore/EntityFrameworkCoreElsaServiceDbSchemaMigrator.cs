using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MERP.ElsaService.Data;
using Volo.Abp.DependencyInjection;

namespace MERP.ElsaService.EntityFrameworkCore;

public class EntityFrameworkCoreElsaServiceDbSchemaMigrator
    : IElsaServiceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreElsaServiceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ElsaServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ElsaServiceDbContext>()
            .Database
            .MigrateAsync();
    }
}
