using GLORIA.API.Entity.Creature;
using System.Collections.Generic;

namespace GLORIA.API.Core.Creature
{
        public interface ICreatureManager
        {
                bool Initialize();

                IEnumerable<ICreatureUnit> GetAll();

                ICreatureUnit GetCreature(string creatureid);

                IEnumerable<ICreatureUnit> GetCreatures(SefiraEnum sefira);

        }
}
