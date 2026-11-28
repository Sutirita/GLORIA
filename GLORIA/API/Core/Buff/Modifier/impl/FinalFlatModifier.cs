namespace GLORIA.API.Core.Buff.Modifier.impl
{
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
   

}
