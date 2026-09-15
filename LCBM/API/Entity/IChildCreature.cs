using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Entity
{
        public interface IChildCreature
        {
                ICreature Parent { get; }

        }
}
