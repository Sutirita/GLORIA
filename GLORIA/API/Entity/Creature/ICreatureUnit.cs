using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Entity.Creature
{
        public interface ICreatureUnit
        {

                float HP { get; }
                bool EscapeAble { get; }
                IEnumerable<ICreatureDerivativeUnit> Children { get; }


        }
}
