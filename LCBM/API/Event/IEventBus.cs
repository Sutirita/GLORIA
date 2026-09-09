using LCBM.API.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Event
{
        public interface IEventBus
        {
                void Subscribe<T>(IModPlugin plugin, EventListener<T> listener) where T : IBaseEvent;

                void UnSubscribe<T>(IModPlugin plugin, EventListener<T> listener) where T : IBaseEvent;

                void Publish<T>(T evt) where T : IBaseEvent;
        }

}
