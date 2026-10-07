using System.Collections.Generic;


namespace GLORIA.API.Core.Mod
{
        public interface IModManager
        {
                bool Initialize(IModlRegistry registry);

                bool IsEnabled(string modGUID);

                bool IsDisabled(string modGUID);

                void Enable(string modGUID);

                void Disable(string modGUID);

                List<string> GetModsOrdering();

        }
}
