using GLORIA.API.Entity.Creature;
using System.Collections.Generic;

namespace GLORIA.API.Core.Creature
{
        public interface ICreatureManager
        {
                bool Initialize();

                IEnumerable<ICreature> GetAll();

                ICreature GetCreature(string creatureid);

                IEnumerable<ICreature> GetCreatures(SefiraEnum sefira);

        }
}
