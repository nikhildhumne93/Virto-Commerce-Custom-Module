using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace Techment.MyEmployeeModule.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "MyEmployeeModule:access";
            public const string Create = "MyEmployeeModule:create";
            public const string Read = "MyEmployeeModule:read";
            public const string Update = "MyEmployeeModule:update";
            public const string Delete = "MyEmployeeModule:delete";

            public static string[] AllPermissions { get; } =
            [
                Access,
                Create,
                Read,
                Update,
                Delete,
            ];
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static SettingDescriptor MyEmployeeModuleEnabled { get; } = new()
            {
                Name = "MyEmployeeModule.Enabled",
                GroupName = "MyEmployeeModule|General",
                ValueType = SettingValueType.Boolean,
                DefaultValue = false,
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return MyEmployeeModuleEnabled;
                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllGeneralSettings;
            }
        }
    }
}
