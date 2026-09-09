using LCBM.API.Event.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Event
{
        public delegate void EventListener<T>(T evt) where T : IBaseEvent;
}
