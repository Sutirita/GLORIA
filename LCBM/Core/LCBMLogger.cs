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

                internal static bool Initialize(LCBaseModPlugin plugin)
                {
                        Logger = plugin.PluginLogger;
                        return true;
                }
                internal static void Shutdown()
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


                public static void Fatal(string message)
                {
                        Logger.LogFatal(message);
                }

                public static void Exception(Exception ex)
                {
                        Logger.LogError(ex.ToString());
                }



        }
}
