using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Entity.Creature
{
        public interface ICreatureDerivative:IUnit
        {
                ICreature Parent { get; }

        }
}
