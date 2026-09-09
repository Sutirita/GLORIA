using LCBM.API.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Event
{
        public interface IEventBus
        {
                void Subscribe<IEvent>(IModPlugin plugin);

                void UnSubscribe<IEvent>(IModPlugin plugin);
        }

}
