using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Event
{
        public interface IEvent
        {
                void OnInvoke();
        }
}
