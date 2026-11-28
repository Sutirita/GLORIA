namespace GLORIA.API.Core.Buff.Modifier.impl
{
        public sealed class FinalMultModifier : IAttrModifier
        {
                public AttrType Stat { get; }

                public IUnitBuff Source { get; }

                public ModifierOperation Type => ModifierOperation.FinalMultiply;

                public float Value { get; }

                public FinalMultModifier(IUnitBuff source, AttrType stat, float value)
                {
                        Source = source;
                        Stat = stat;
                        Value = value;
                }
        }
   

}
