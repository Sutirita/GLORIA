using HarmonyLib;
using GLORIA.Asset;
using GLORIA.Core;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
namespace GLORIA.Harmony
{
        internal static class HarmonyManager
        {
                private static HarmonyLib.Harmony _harmony;

                public static bool Initialize()
                {

                        bool flag = false;
                        try
                        {
                                _harmony = new HarmonyLib.Harmony(StaticData.PLUGIN_GUID);

                                _harmony.PatchAll(typeof(HP_Basic));

                                _harmony.PatchAll(typeof(HP_Basic.LoadResearchDescData_LogPatch));

                                _harmony.PatchAll(typeof(HP_InfoDetails));


                                flag = true;
                        }
                        catch (Exception e)
                        {
                                Logger.Fatal("Failed to initialize HarmonyPatch.");
                                Logger.Exception(e);
                        }

                        return flag;
                }

                public static void Shutdown()
                {
                        _harmony?.UnpatchSelf();
                        _harmony = null;
                }

        }
}
