using MERP.ElsaService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MERP.ElsaService;

[DependsOn(
    typeof(ElsaServiceEntityFrameworkCoreTestModule)
    )]
public class ElsaServiceDomainTestModule : AbpModule
{

}
