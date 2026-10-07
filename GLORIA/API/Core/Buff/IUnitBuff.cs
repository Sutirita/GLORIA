using GLORIA.API.Entity;
using System.Collections.Generic;

namespace GLORIA.API.Core.Buff
{
        public enum BuffType
        {
                Neutral,
                Positive,
                Negative
        }

        public interface IUnitBuff
        {
                IUnit Owner { get; }
                string Id { get; }
                BuffType BuffType { get; }
                bool Stackable { get; }
                int MaxStack { get; }
                IEnumerable<IStatModifier> StatModifiers { get; }

        }

        public interface ITickableBuff : IUnitBuff, ITickable
        {
                float Duration { get; }
                float RemainTime { get; set; }
        }

}