using GLORIA.API.Entity;


namespace GLORIA.API.Event
{
        public interface IOfficerEvent:IBaseEvent
        {
                IOfficer Officer { get; }
        }
}
