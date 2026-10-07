using GLORIA.API.Core.Mod;
using GLORIA.Event;
namespace GLORIA.API.Event
{
        public interface IEventBus
        {
                bool Initialize();
                void Subscribe<T>(IMod plugin, EventListener<T> listener) where T : IBaseEvent;

                void Unsubscribe<T>(IMod plugin, EventListener<T> listener) where T : IBaseEvent;

                void Publish<T>(T evt) where T : IBaseEvent;
        }

}
