using System;
using System.IO;

namespace GLORIA
{
        internal class StaticData
        {
                //plugininfo
                public const string PLUGIN_GUID = "com.Sutirita.LobotomyCropBaseMod";

                public const string PLUGIN_NAME = "GLORIA";

                public const string PLUGIN_VERSION = "0.2.8";

                public static string PluginVertionDescStr = $"\n{PLUGIN_NAME} {PLUGIN_VERSION}ver\nMade by Sutirita.";

                //Dir

                public static readonly string PluginRootDirPath = Path.Combine(BepInEx.Paths.PluginPath, "Sutirita-GLORIA");

                public static readonly string DataDirPath = Path.Combine(PluginRootDirPath, "Data");

                public static readonly string LibDataDirPath = Path.Combine(DataDirPath, "lib");

                public static readonly string SaveBackUpDirPath = Path.Combine(PluginRootDirPath, "backup");




                //res

                public const string ConfigDescRes = "GLORIA.Properties.ConfigDesc.xml";







                //必须手动传入这个，不然某些东西会尝试使用 Array.Empty<T>()，而LC的.net不支持这样做 
                public static object[] EmptyObjList = { };





                //Text

                public const string CONSOLE_PLACEHOLDER = "Enter Command...";


                public const string DESC_NO_FOUND = "Desc No Found!";

                public const string DEFAULT_LANG = "cn";

        }




}
