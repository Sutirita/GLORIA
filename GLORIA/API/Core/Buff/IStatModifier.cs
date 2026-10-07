

namespace GLORIA.API.Core.Buff
{


        public enum OperationType
        {
                Add,
                Mult
        }

        public interface IStatModifier
        {
                StatId StatID { get; }
                OperationType OperationType { get; }

                float Value { get; }

        }

}
