
namespace GLORIA.API.Core
{
        public interface IMod
        {
                string GUID { get; }
                void Initialize(IModInfo context);
        }
}
