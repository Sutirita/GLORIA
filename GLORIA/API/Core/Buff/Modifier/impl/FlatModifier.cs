namespace GLORIA.API.Core.Buff.Modifier.impl
{
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
   

}
