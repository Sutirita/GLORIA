using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Entity.Creature
{
        public interface ICreature:IUnit
        {
                RiskLevel RiskLevel { get; }
                float HP { get; }
                bool EscapeAble { get; }
                IEnumerable<ICreatureDerivative> Children { get; }

        }
}
