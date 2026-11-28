namespace GLORIA.API.Core.Buff
{
        public interface IStackableBuff : IUnitBuff
        {
                int MaxStack { get; }
                int CurrentStack { get; }
                void SetStack(int stack);
        }

}