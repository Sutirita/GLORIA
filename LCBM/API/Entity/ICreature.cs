using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Entity
{
        internal interface ICreature
        {
                IEnumerable<IChildCreature> Children { get; }


        }
}
