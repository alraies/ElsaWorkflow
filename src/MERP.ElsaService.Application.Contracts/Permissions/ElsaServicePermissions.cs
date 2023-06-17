namespace MERP.ElsaService.Permissions;

public static class ElsaServicePermissions
{
    public const string GroupName = "ElsaService";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public const string ElsaDashboard = GroupName + ".ElsaDashboard";
    
    public class Employee
    {
        public const string Default = GroupName + ".Employee";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";

    }

    public class Absence
    {
        public const string Default = GroupName + ".Absence";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
