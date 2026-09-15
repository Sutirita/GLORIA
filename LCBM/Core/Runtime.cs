using GLORIA.Asset;
using GLORIA.Harmony;
namespace GLORIA.Core
{
        internal class Runtime
        {
               private  static MainPlugin _plugin;

                public static bool Initialize(MainPlugin lCBaseMod )
                {
                        _plugin = lCBaseMod;

                        bool flag = true;

                        flag = flag && Logger.Initialize(_plugin);

                        flag = flag && AssetManager.Initialize();

                        flag = flag && ConfigManual.Initialize(_plugin);

                        flag = flag && HarmonyManager.Initialize();


                        return flag;

                }

                public static void Shutdown()
                {
                        HarmonyManager.Shutdown();

                        ConfigManual.Shutdown();

                        AssetManager.Shutdown();

                        Logger.Shutdown();

                }
        }
}
