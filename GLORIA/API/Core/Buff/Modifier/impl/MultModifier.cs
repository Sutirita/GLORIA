namespace GLORIA.API.Core.Buff.Modifier.impl
{
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
   

}
