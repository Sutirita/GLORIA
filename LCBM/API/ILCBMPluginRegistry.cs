
using Mono.Collections.Generic;
using System.Collections.Generic;

namespace LCBM.API
{
        interface ILCBMPluginRegistry
        {
                ReadOnlyCollection<ILCBaseModPlugin> Plugins { get; }

                ILCBaseModPlugin Get(string guid);

                bool IsRegistered(string guid);
        }
}