using GLORIA.API.Entity;

namespace GLORIA.API.Event
{
        interface IAgentEvent : IBaseEvent
        {
                IAgent Agent { get; }
        }
}
