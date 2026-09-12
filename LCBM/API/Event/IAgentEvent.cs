using LCBM.API.Entity;

namespace LCBM.API.Event
{
        interface IAgentEvent : IBaseEvent
        {
                IAgent Agent { get; }
        }
}
