using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API
{
        interface ILCBaseModPlugin
        {
                string DisPlayName { get; }
                string GUID { get; }
                void Initialize(ILCBaseModContext context);
        }
}
