
using System.Collections.Generic;

using LCBM.API.Core;

namespace LCBM.Core
{
        public class LCBMPluginRegistry : IModluginRegistry
        {
                private static Dictionary<string, IModPlugin> _pluginLib = new Dictionary<string, IModPlugin>();

                IEnumerable<IModPlugin> IModluginRegistry.Plugins => _pluginLib.Values;


                internal static void Initialize()
                {
                        _pluginLib.Clear();


                }


                IModPlugin IModluginRegistry.Get(string guid)
                {
                        if (!_pluginLib.TryGetValue(guid, out IModPlugin plugin)) return null;
                        return plugin;
                }

                bool IModluginRegistry.IsRegistered(string guid)
                {
                        if (!_pluginLib.TryGetValue(guid, out IModPlugin plugin)) return false;

                        return _pluginLib is null;
                }
        }
}
