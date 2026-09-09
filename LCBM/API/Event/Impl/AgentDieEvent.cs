using LCBM.API.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LCBM.API.Event.Impl
{
        internal class AgentDieEvent:IEvent
        {
                public delegate void AgentDiedEventHandler(IAgent agent);

                event AgentDiedEventHandler EventHandler;

                void OnSubscribe(AgentDiedEventHandler handler)
                {
                        EventHandler += handler;
                }

                public void OnInvoke()
                {
                        throw new NotImplementedException();
                }
        }
}
