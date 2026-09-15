using GLORIA.API.Entity;

namespace GLORIA.API.Event
{
        public interface ICreatureEvent:IBaseEvent
        {
                ICreature Creature { get; }
        }
}
