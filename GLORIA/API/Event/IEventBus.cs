using GLORIA.API.Core;
using GLORIA.Event;
namespace GLORIA.API.Event
{
        public interface IEventBus
        {
                void Subscribe<T>(IModPlugin plugin, EventListener<T> listener) where T : IBaseEvent;

                void Unsubscribe<T>(IModPlugin plugin, EventListener<T> listener) where T : IBaseEvent;

                void Publish<T>(T evt) where T : IBaseEvent;
        }

}
