using LCBM.API;
using Mono.Collections.Generic;
using System;
using System.Collections.Generic;


namespace LCBM.LCBM.Core
{
        internal class LCBMPluginRegistry : ILCBMPluginRegistry
        {
                static Dictionary<string,ILCBaseModPlugin> _pluginLib = new Dictionary<string, ILCBaseModPlugin> ();

                ReadOnlyCollection<ILCBaseModPlugin> ILCBMPluginRegistry.Plugins
                {
                        get
                        {
                                return null;
                        }
                }

                internal static void Initialize()
                {



                }
                ILCBaseModPlugin ILCBMPluginRegistry.Get(string guid)
                {
                        if(!_pluginLib.TryGetValue(guid, out ILCBaseModPlugin plugin)) return null;
                        return plugin;
                }

                bool ILCBMPluginRegistry.IsRegistered(string guid)
                {
                        if (!_pluginLib.TryGetValue(guid, out ILCBaseModPlugin plugin)) return false;

                        return _pluginLib is null; 
                }
        }
}
