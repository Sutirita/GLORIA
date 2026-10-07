using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GLORIA.API.Core.Mod
{
        public interface IModlRegistry
        {
                IEnumerable<IMod> Mods { get; }

                IMod Get(string guid);

                void Register(IMod plugin);

                bool IsRegistered(string guid);
        }
}