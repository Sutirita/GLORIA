
namespace GLORIA.API.Core
{
        public interface ITickable
        {
                bool IsPaused { get; }
                void Tick(float deltaTime);
        }

}
