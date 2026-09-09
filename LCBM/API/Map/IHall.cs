using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Map
{
        interface IHall
        {

                //走廊上下方只能与收容室相连
                //走廊左右方不能与收容室相连

                IHall GetHall(LEFTRIGHT direction);
                IMapRoom GetRoom(LEFTRIGHT direction);
                IElevator GetElevator(LEFTRIGHT direction);
                IEnumerable<IContainRoom> GetContainRooms(UPDOWN direction);
        }
}
