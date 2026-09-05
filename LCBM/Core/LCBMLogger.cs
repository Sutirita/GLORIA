using BepInEx.Logging;
using LCBM;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.Core
{
        internal class LCBMLogger
        {

                private static ManualLogSource Logger;

                public static void Initialize(LCBaseModPlugin plugin)
                {
                        Logger = plugin.PluginLogger;

                }
                public static void Shutdown()
                {

                }


                public static void Message(string message)
                {
                        Logger.LogMessage(message);
                }


                public static void Info(string message)
                {
                        Logger.LogInfo(message);
                }

                public static void Error(string message)
                {
                        Logger.LogError(message);

                }
                public static void Warning(string message)
                {
                        Logger.LogWarning(message);

                }



        }
}
