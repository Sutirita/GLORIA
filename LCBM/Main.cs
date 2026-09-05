using System;
using BepInEx;
using BepInEx.Logging;
using LCBM.Core;


namespace LCBM
{
        [BepInPlugin(LCBMStaticData.PLUGIN_GUID,LCBMStaticData.PLUGIN_NAME,LCBMStaticData.PLUGIN_VERSION)]
        [BepInProcess("LobotomyCorp.exe")]
        public sealed class LCBaseModPlugin : BaseUnityPlugin
        {
                internal ManualLogSource PluginLogger => Logger;

                // 在插件启动时会直接调用Awake()方法
                void Awake()
                {
                        try
                        {
                                //初始化
                                LCBMRuntime.Initialize(this);
                        }
                        catch (Exception ex)
                        {
                                Logger.LogError($"Initialization failed: {ex.Message}\n{ex.StackTrace}");
                        }
                }

                //关闭
                void OnDestroy()
                {
                        LCBMRuntime.Shutdown();
                }




        }


}

