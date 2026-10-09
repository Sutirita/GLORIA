

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


        public interface IAttrModifier
        {
                IUnitBuff Source { get; }

                ModifierOperation Type { get; }

                AttrType Stat { get; }

                float Value { get; }
        }



        public sealed class FlatModifier : IAttrModifier
        {
                public AttrType Stat { get; }

                public ModifierOperation Type => ModifierOperation.Add;

                public IUnitBuff Source { get; }

                public float Value { get; }

                public FlatModifier(IUnitBuff source, AttrType stat, float value)
                {
                        Source = source;
                        Stat = stat;
                        Value = value;
                }

        }

        public sealed class FinalFlatModifier : IAttrModifier
        {
                public AttrType Stat { get; }

                public ModifierOperation Type => ModifierOperation.FinalAdd;

                public IUnitBuff Source { get; }

                public float Value { get; }

                public FinalFlatModifier(IUnitBuff source, AttrType stat, float value)
                {
                        Source = source;
                        Stat = stat;
                        Value = value;
                }

        }

        public sealed class MultModifier : IAttrModifier
        {
                public AttrType Stat { get; }

                public IUnitBuff Source { get; }

                public ModifierOperation Type => ModifierOperation.Multiply;

                public float Value { get; }

                public MultModifier(IUnitBuff source, AttrType stat, float value)
                {
                        Source = source;
                        Stat = stat;
                        Value = value;
                }
        }
        public sealed class FinalMultModifier : IAttrModifier
        {
                public AttrType Stat { get; }

                public IUnitBuff Source { get; }

                public ModifierOperation Type => ModifierOperation.Multiply;

                public float Value { get; }

                public FinalMultModifier(IUnitBuff source, AttrType stat, float value)
                {
                        Source = source;
                        Stat = stat;
                        Value = value;
                }
        }






        public sealed class StatContext
        {
                public IUnit Unit { get; }

                public AttrType Stat { get; }

                public float BaseValue { get; }

                public float Value { get; private set; }

                public StatContext(IUnit unit, AttrType stat, float baseValue)
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
