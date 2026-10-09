using GLORIA.API.Entity;
using System.Collections.Generic;

namespace GLORIA.API.Core.Buff
{
        public interface IBuffManager
        {
                bool Initialize();

                bool IsBuffedUnit(IUnit unit);

                bool HasBuff(IUnit unit, string buffid);

                void AddBuff(IUnit unit, IUnitBuff buff, int stack = 1);

                void RemoveBuff(IUnit unit, string buffId, int stack = 1);

                bool TryGetBuff(IUnit unit, string buffid, out IUnitBuff buff);

                int GetBuffStack(IUnit unit, string buffid);



                bool TryGetAttrBonus(IUnit unit, AttrType type, out IEnumerable<IAttrModifier> modifiers);

                bool TryGetAttrWithBonus(IUnit unit, AttrType type, out float Value);
        }
}
