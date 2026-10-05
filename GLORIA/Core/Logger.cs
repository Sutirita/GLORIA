using BepInEx.Logging;
using GLORIA;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.Core
{
        internal class Logger
        {

                private static ManualLogSource _logger;

                internal static bool Initialize(MainPlugin plugin)
                {
                        _logger = plugin.PluginLogger;
                        return true;
                }
                internal static void Shutdown()
                {
                        
                }


                public static void Message(string message)
                {
                        _logger.LogMessage(message);
                }


                public static void Info(string message)
                {
                        _logger.LogInfo(message);
                }

                public static void Error(string message)
                {
                        _logger.LogError(message);

                }
                public static void Warning(string message)
                {
                        _logger.LogWarning(message);

                }


                public static void Fatal(string message)
                {
                        _logger.LogFatal(message);
                }

                public static void Exception(Exception ex)
                {
                        _logger.LogError(ex.ToString());
                }



        }
}
