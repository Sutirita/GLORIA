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
                IUnit Owner { get;}
                string Id { get; }
                BuffType BuffType { get; }
                IEnumerable<IAttrModifier> StatModifiers { get; }

        }

        public interface IStackableBuff : IUnitBuff
        {
                int MaxStack { get; }
                int CurrentStack { get; }
                void SetStack(int stack);
        }





        public interface ITickableBuff : IUnitBuff, ITickable
        {
                float Duration { get; }
                float RemainTime { get; set; }
        }

}