
namespace GLORIA.API.Core.Mod
{
        public interface IModInfo
        {
                string DisplayName { get; }

                string Version { get; }

                string Authors { get; }

                string Desc { get; }

                string UpdateURL { get; }

                string LogoFileRes { get; }
        }
}
