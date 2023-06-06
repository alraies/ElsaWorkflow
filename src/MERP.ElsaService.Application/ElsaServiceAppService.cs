using System;
using System.Collections.Generic;
using System.Text;
using MERP.ElsaService.Localization;
using Volo.Abp.Application.Services;

namespace MERP.ElsaService;

/* Inherit your application services from this class.
 */
public abstract class ElsaServiceAppService : ApplicationService
{
    protected ElsaServiceAppService()
    {
        LocalizationResource = typeof(ElsaServiceResource);
    }
}
