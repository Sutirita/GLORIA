namespace GLORIA.API.Core.Buff
{
        public interface ITickableBuff : IUnitBuff, ITickable
        {
                float Duration { get; }
                float RemainTime { get; set; }
        }

}