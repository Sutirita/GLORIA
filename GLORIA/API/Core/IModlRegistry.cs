
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GLORIA.API.Core
{
        public interface IModlRegistry
        {
                IEnumerable<IMod> Mods { get; }

                IMod Get(string guid);

                void Registet(IMod plugin);

                bool IsRegistered(string guid);
        }
}