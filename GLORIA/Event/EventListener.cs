using GLORIA.API.Event;
namespace GLORIA.Event
{
        public delegate void EventListener<T>(T evt) where T : IBaseEvent;
}
