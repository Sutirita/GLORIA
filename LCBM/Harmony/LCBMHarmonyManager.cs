using HarmonyLib;
namespace LCBM.Harmony
{
        internal static class LCBMHarmonyManager
        {
                private static HarmonyLib.Harmony _harmony;

                public static void Initialize()
                {
                        _harmony = new HarmonyLib.Harmony(LCBMStaticData.PLUGIN_GUID);

                        /*
                            _harmony.PatchAll(typeof(LCBMHarmonyManager).Assembly);
                         */

                }

                public static void Shutdown()
                {
                        _harmony?.UnpatchSelf();
                        _harmony = null;
                }

        }
}
