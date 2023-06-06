using MERP.ElsaService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MERP.ElsaService.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ElsaServicePageModel : AbpPageModel
{
    protected ElsaServicePageModel()
    {
        LocalizationResourceType = typeof(ElsaServiceResource);
    }
}
