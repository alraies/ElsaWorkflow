using System.Threading.Tasks;

namespace MERP.ElsaService.Data;

public interface IElsaServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
