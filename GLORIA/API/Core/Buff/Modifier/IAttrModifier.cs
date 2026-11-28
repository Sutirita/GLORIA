using System.Collections.Generic;
using System;

namespace GLORIA.API.Core.Buff.Modifier
{
        public interface IAttrModifier
        {
                IUnitBuff Source { get; }

                ModifierOperation Type { get; }

                AttrType Stat { get; }

                float Value { get; }
        }
}
