using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Entity.Creature
{
        public interface ICreatureDerivativeUnit:IUnit
        {
                ICreatureUnit Parent { get; }

        }
}
