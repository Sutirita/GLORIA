
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GLORIA.API.Core
{
        public interface IModluginRegistry
        {
                IEnumerable<IModPlugin> Plugins { get; }

                IModPlugin Get(string guid);

                bool IsRegistered(string guid);
        }
}