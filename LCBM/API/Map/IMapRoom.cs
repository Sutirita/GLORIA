using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Map
{
        interface IMapRoom
        {
                IHall GetHall(LEFTRIGHT direction);
                IMapRoom GetRoom(LEFTRIGHT direction);
                IElevator GetElevator(LEFTRIGHT direction);
        }
}
