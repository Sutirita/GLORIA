using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Map
{
        public interface IElevator
        {
                IHall GetHall(LEFTRIGHT direction);
                IMapRoom GetRoom(LEFTRIGHT direction);
                IElevator GetElevator(UPDOWN direction);
        }
}
