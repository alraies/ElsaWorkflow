using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MERP.ElsaService.Data;

/* This is used if database provider does't define
 * IElsaServiceDbSchemaMigrator implementation.
 */
public class NullElsaServiceDbSchemaMigrator : IElsaServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
