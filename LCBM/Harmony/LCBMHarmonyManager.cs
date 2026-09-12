using HarmonyLib;
using LCBM.Asset;
using LCBM.Core;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
namespace LCBM.Harmony
{
        internal static class LCBMHarmonyManager
        {
                private static HarmonyLib.Harmony _harmony;

                public static bool Initialize()
                {

                        bool flag = false;
                        try
                        {
                                _harmony = new HarmonyLib.Harmony(LCBMStaticData.PLUGIN_GUID);

                                _harmony.PatchAll(typeof(HP_Basic));

                                _harmony.PatchAll(typeof(HP_Basic.LoadResearchDescData_LogPatch));

                                _harmony.PatchAll(typeof(HP_InfoDetails));


                                flag = true;
                        }
                        catch (Exception e)
                        {
                                LCBMLogger.Fatal("Failed to initialize HarmonyPatch.");
                                LCBMLogger.Exception(e);
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
