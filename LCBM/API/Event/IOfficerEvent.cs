using LCBM.API.Entity;


namespace LCBM.API.Event
{
        public interface IOfficerEvent:IBaseEvent
        {
                IOfficer Officer { get; }
        }
}
