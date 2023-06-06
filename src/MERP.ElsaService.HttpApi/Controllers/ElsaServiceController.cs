using MERP.ElsaService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MERP.ElsaService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ElsaServiceController : AbpControllerBase
{
    protected ElsaServiceController()
    {
        LocalizationResource = typeof(ElsaServiceResource);
    }
}
