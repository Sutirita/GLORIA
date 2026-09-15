using System;
using BepInEx;
using BepInEx.Logging;
using GLORIA.Core;


namespace GLORIA
{
        [BepInPlugin(StaticData.PLUGIN_GUID, StaticData.PLUGIN_NAME, StaticData.PLUGIN_VERSION)]
        [BepInProcess("LobotomyCorp.exe")]
        public sealed class MainPlugin : BaseUnityPlugin
        {
                internal ManualLogSource PluginLogger => Logger;

                void Awake()
                {
                        Logger.LogInfo("Initializing...");
                        bool flag = Runtime.Initialize(this);
                        if (!flag) Logger.LogFatal("Failed to initialize Runtime.");

                }


                //关闭
                void OnDestroy()
                {
                        Runtime.Shutdown();
                }




        }


}

