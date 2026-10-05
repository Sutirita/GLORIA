
using System.Collections.Generic;

using GLORIA.API.Core;

namespace GLORIA.Core
{
        internal class PluginRegistry : IModlRegistry
        {
                private static readonly Dictionary<string, IMod> _modLib = new Dictionary<string, IMod>();

                IEnumerable<IMod> IModlRegistry.Mods => _modLib.Values;

                public void Registet(IMod plugin)
                {
                        if(_modLib.ContainsKey(plugin.GUID))
                        {
                                Logger.Error("The ");
                        }

                        _modLib[plugin.GUID] = plugin;
                }

                IMod IModlRegistry.Get(string guid)
                {
                        if (!_modLib.TryGetValue(guid, out IMod plugin)) return null;
                        return plugin;
                }

                bool IModlRegistry.IsRegistered(string guid)
                {
                        if (!_modLib.TryGetValue(guid, out IMod plugin)) return false;

                        return plugin is null;
                }
        }
}
