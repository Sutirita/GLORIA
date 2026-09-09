using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Map
{
        internal interface IContainRoom
        {
                //只能与走廊相连
                IHall HallConnected { get; }
        }
}
