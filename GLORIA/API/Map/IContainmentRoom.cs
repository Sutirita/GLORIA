using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Map
{
        internal interface IContainmentRoom
        {
                //只能与走廊相连
                IHall HallConnected { get; }
        }
}
