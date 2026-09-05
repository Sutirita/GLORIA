using System.IO;

namespace LCBM
{
        internal class LCBMStaticData
        {
                public const string PLUGIN_GUID = "com.Sutirita.LobotomyCropBaseMod";

                public const string PLUGIN_NAME = "LCBaseMod";

                public const string PLUGIN_VERSION = "0.2.7";

                public static string PluginVertionDescStr = $"\n{PLUGIN_NAME} {PLUGIN_VERSION}ver\nMade by Sutirita.";

                public static readonly string PluginRootDirPath = Path.Combine(BepInEx.Paths.PluginPath, "Sutirita-LCBaseMod");

                public static readonly string DataDirPath = Path.Combine(PluginRootDirPath, "Data");

                public static readonly string LibDataDirPath = Path.Combine(DataDirPath, "lib");

                public static readonly string SaveBackUpDirPath = Path.Combine(PluginRootDirPath, "backup");

                public static string ConfigDescRes = "LCBM.Assets.LCBMConfigDesc.xml";


                public const string DESC_NO_FOUND = "Desc No Found!";

                public static object[] EmptyObjList = { };


                public const string CONSOLE_PLACEHOLDER = "Enter Command...";

                public const string DEFAULT_LANG = "cn";

        }




}
