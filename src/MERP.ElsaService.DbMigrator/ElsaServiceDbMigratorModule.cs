using MERP.ElsaService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MERP.ElsaService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ElsaServiceEntityFrameworkCoreModule),
    typeof(ElsaServiceApplicationContractsModule)
    )]
public class ElsaServiceDbMigratorModule : AbpModule
{

}
