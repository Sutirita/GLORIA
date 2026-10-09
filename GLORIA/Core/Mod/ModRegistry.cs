
using System.Collections.Generic;
using GLORIA.API.Core.Mod;

namespace GLORIA.Core.Mod
{
        internal class ModRegistry : IModlRegistry
        {
                private static readonly Dictionary<string, IMod> _modLib = new Dictionary<string, IMod>();

                IEnumerable<IMod> IModlRegistry.Mods => _modLib.Values;

                public bool Initialize()
                {
                        return true;
                }

                public void Register(IMod mod)
                {
                        if(_modLib.ContainsKey(mod.GUID))
                        {
                                Logger.Error($"The mod:{mod.GUID} is already exsist.");
                                return;
                        }
                        _modLib[mod.GUID] = mod;
                }

                IMod IModlRegistry.Get(string guid)
                {
                        if (!_modLib.TryGetValue(guid, out IMod mod)) return null;
                        return mod;
                }

                bool IModlRegistry.IsRegistered(string guid)
                {
                        if (!_modLib.TryGetValue(guid, out IMod mod)) return false;

                        return mod is null;
                }
        }
}
