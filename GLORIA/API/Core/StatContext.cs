

using GLORIA.API.Entity;

namespace GLORIA.API.Core.Buff.Modifier.impl
{
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
