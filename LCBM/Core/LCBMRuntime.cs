using LCBM.Asset;
using LCBM.Harmony;
namespace LCBM.Core
{
        internal class LCBMRuntime
        {
               private  static LCBaseModPlugin _plugin;

                public static bool Initialize(LCBaseModPlugin lCBaseMod )
                {
                        _plugin = lCBaseMod;

                        bool flag = true;

                        flag = flag && LCBMLogger.Initialize(_plugin);

                        flag = flag && LCBMAssetManager.Initialize();

                        flag = flag && LCBMConfig.Initialize(_plugin);

                        flag = flag && LCBMHarmonyManager.Initialize();


                        return flag;

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
