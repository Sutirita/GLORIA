using System;
using BepInEx;
using BepInEx.Logging;
using LCBM.Core;


namespace LCBM
{
        [BepInPlugin(LCBMStaticData.PLUGIN_GUID, LCBMStaticData.PLUGIN_NAME, LCBMStaticData.PLUGIN_VERSION)]
        [BepInProcess("LobotomyCorp.exe")]
        public sealed class LCBaseModPlugin : BaseUnityPlugin
        {
                internal ManualLogSource PluginLogger => Logger;

                void Awake()
                {
                        Logger.LogInfo("Initializing...");
                        bool flag = LCBMRuntime.Initialize(this);
                        if (!flag) Logger.LogFatal("Failed to initialize Runtime.");

                }


                //关闭
                void OnDestroy()
                {
                        LCBMRuntime.Shutdown();
                }




        }


}

