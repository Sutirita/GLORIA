using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Map
{
        interface IElevator
        {
                IHall GetHall(LEFTRIGHT direction);
                IMapRoom GetRoom(LEFTRIGHT direction);
                IElevator GetElevator(UPDOWN direction);
        }
}
