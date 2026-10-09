using GLORIA.API.Entity.Worker;


namespace GLORIA.API.Event
{
        public interface IOfficerEvent:IBaseEvent
        {
                IOfficer Officer { get; }
        }
}
