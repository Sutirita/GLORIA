namespace GLORIA.API.Core.Mod
{
        public interface IMod
        {
                string GUID { get; }
                void Initialize(IModInfo context);
        }
}
