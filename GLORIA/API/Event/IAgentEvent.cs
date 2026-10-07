using GLORIA.API.Entity.Worker;

namespace GLORIA.API.Event
{
        public interface IAgentEvent : IBaseEvent
        {
                IAgentUnit Agent { get; }
        }







}
