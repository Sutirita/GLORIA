using LCBM.API.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Event.Impl
{
        internal class LCBMEventBus : IEventBus
        {

                Dictionary<IEvent, List<IModPlugin>> Subcribers;

                public void Subscribe<IEvent>(IModPlugin plugin)
                {
                        throw new NotImplementedException();
                }



                public void UnSubscribe<IEvent>(IModPlugin plugin)
                {
                        throw new NotImplementedException();
                }
        }
}
