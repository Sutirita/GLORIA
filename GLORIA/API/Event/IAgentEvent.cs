using GLORIA.API.Entity;

namespace GLORIA.API.Event
{
        public interface IAgentEvent : IBaseEvent
        {
                IAgent Agent { get; }
        }







}
