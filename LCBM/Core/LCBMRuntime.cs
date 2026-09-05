using LCBM.Asset;
using LCBM.Harmony;
namespace LCBM.Core
{
        internal class LCBMRuntime
        {
               private  static LCBaseModPlugin _plugin;

                public static void Initialize(LCBaseModPlugin lCBaseMod )
                {
                        _plugin = lCBaseMod;

                       
                        LCBMLogger.Initialize(_plugin);

                        LCBMAssetManager.Initialize();

                        LCBMConfig.Initialize(_plugin);

                        LCBMHarmonyManager.Initialize();
                }

                public static void Shutdown()
                {
                        LCBMHarmonyManager.Shutdown();

                        LCBMConfig.Shutdown();

                        LCBMAssetManager.Shutdown();

                        LCBMLogger.Shutdown();

                }
        }
}
