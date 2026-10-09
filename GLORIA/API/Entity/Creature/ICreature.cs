using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Entity.Creature
{
        public interface ICreature
        {

                float HP { get; }
                bool EscapeAble { get; }
                IEnumerable<ICreatureDerivative> Children { get; }

        }
}
