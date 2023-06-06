using Volo.Abp.Settings;

namespace MERP.ElsaService.Settings;

public class ElsaServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ElsaServiceSettings.MySetting1));
    }
}
