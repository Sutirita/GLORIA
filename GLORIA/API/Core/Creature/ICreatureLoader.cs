using GLORIA.API.Entity.Creature;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Core.Creature
{
        public interface ICreatureLoader
        {
                void Load(ICreatureUnit creature);


        }
}
