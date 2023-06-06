using Volo.Abp.Modularity;

namespace MERP.ElsaService;

[DependsOn(
    typeof(ElsaServiceApplicationModule),
    typeof(ElsaServiceDomainTestModule)
    )]
public class ElsaServiceApplicationTestModule : AbpModule
{

}
