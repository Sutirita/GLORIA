using GLORIA.API.Entity.Creature;

namespace GLORIA.API.Event
{
        public interface ICreatureEvent:IBaseEvent
        {
                ICreatureUnit Creature { get; }
        }
}
