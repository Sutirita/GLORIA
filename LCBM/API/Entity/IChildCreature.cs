using System;
using System.Collections.Generic;
using System.Text;

namespace LCBM.API.Entity
{
        public interface IChildCreature
        {
                ICreature Parent { get; }

        }
}
