using GLORIA.API.Core;
using GLORIA.Event;
namespace GLORIA.API.Event
{
        public interface IEventBus
        {
                void Subscribe<T>(IMod plugin, EventListener<T> listener) where T : IBaseEvent;

                void Unsubscribe<T>(IMod plugin, EventListener<T> listener) where T : IBaseEvent;

                void Publish<T>(T evt) where T : IBaseEvent;
        }

}
