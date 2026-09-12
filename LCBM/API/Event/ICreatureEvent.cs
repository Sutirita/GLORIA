using LCBM.API.Entity;

namespace LCBM.API.Event
{
        public interface ICreatureEvent:IBaseEvent
        {
                ICreature Creature { get; }
        }
}
