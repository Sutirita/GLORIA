using LCBM.API.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Core
{
        public interface IModPlugin
        {
                IEventBus EventBus { get; }
                string DisPlayName { get; }
                string GUID { get; }
                void Initialize(IModContext context);
        }
}
