
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LCBM.API.Core
{
        public interface IModluginRegistry
        {
                IEnumerable<IModPlugin> Plugins { get; }

                IModPlugin Get(string guid);

                bool IsRegistered(string guid);
        }
}