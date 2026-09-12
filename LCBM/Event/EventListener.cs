using LCBM.API.Event;
namespace LCBM.Event
{
        public delegate void EventListener<T>(T evt) where T : IBaseEvent;
}
