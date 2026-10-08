

using GLORIA.API.Entity;
using System.Collections.Generic;
using System;

namespace GLORIA.API.Core.Buff
{
        public enum ModifierOperation
        {
                Add,
                Multiply,
                FinalAdd,
                FinalMultiply
        }


        public interface IStatModifier
        {
                IUnitBuff Source { get; }

                ModifierOperation Type { get; }

                StatType Stat { get; }

                float Value { get; }
        }



        public sealed class FlatModifier : IStatModifier
        {
                public StatType Stat { get; }

                public ModifierOperation Type => ModifierOperation.Add;

                public IUnitBuff Source { get; }

                public float Value { get; }

                public FlatModifier(IUnitBuff source, StatType stat, float value)
                {
                        Source = source;
                        Stat = stat;
                        Value = value;
                }

        }

        public sealed class FinalFlatModifier : IStatModifier
        {
                public StatType Stat { get; }

                public ModifierOperation Type => ModifierOperation.FinalAdd;

                public IUnitBuff Source { get; }

                public float Value { get; }

                public FinalFlatModifier(IUnitBuff source, StatType stat, float value)
                {
                        Source = source;
                        Stat = stat;
                        Value = value;
                }

        }

        public sealed class MultModifier : IStatModifier
        {
                public StatType Stat { get; }

                public IUnitBuff Source { get; }

                public ModifierOperation Type => ModifierOperation.Multiply;

                public float Value { get; }

                public MultModifier(IUnitBuff source, StatType stat, float value)
                {
                        Source = source;
                        Stat = stat;
                        Value = value;
                }
        }
        public sealed class FinalMultModifier : IStatModifier
        {
                public StatType Stat { get; }

                public IUnitBuff Source { get; }

                public ModifierOperation Type => ModifierOperation.Multiply;

                public float Value { get; }

                public FinalMultModifier(IUnitBuff source, StatType stat, float value)
                {
                        Source = source;
                        Stat = stat;
                        Value = value;
                }
        }






        public sealed class StatContext
        {
                public IUnit Unit { get; }

                public StatType Stat { get; }

                public float BaseValue { get; }

                public float Value { get; private set; }

                public StatContext(IUnit unit, StatType stat, float baseValue)
                {
                        Unit = unit;
                        Stat = stat;
                        BaseValue = baseValue;
                        Value = baseValue;
                }

                internal void SetValue(float value)
                {
                        Value = value;
                }
                
        }
   

}
