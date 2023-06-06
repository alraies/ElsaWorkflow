using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace MERP.ElsaService.Web;

[Dependency(ReplaceServices = true)]
public class ElsaServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ElsaService";
}
