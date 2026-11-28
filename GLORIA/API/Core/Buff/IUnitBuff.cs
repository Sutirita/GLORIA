using GLORIA.API.Core.Buff.Modifier;
using GLORIA.API.Entity;
using System.Collections.Generic;

namespace GLORIA.API.Core.Buff
{

        public interface IUnitBuff
        {
                IUnit Owner { get;}
                string Id { get; }
                BuffType BuffType { get; }
                IEnumerable<IAttrModifier> StatModifiers { get; }

        }

}