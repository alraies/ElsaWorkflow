using MERP.ElsaService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MERP.ElsaService.Permissions;

public class ElsaServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ElsaServicePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ElsaServicePermissions.MyPermission1, L("Permission:MyPermission1"));
        myGroup.AddPermission(ElsaServicePermissions.ElsaDashboard, L("Permission:ElsaDashboard"));

        var absencePermission = myGroup.AddPermission(ElsaServicePermissions.Absence.Default, L("Permission:Absence"));
        absencePermission.AddChild(ElsaServicePermissions.Absence.Create, L("Permission:Create"));
        absencePermission.AddChild(ElsaServicePermissions.Absence.Update, L("Permission:Update"));
        absencePermission.AddChild(ElsaServicePermissions.Absence.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ElsaServiceResource>(name);
    }
}
