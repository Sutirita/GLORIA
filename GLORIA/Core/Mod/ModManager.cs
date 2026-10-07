using GLORIA.API.Core.Mod;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.Core.Mod
{
        internal class ModManager : IModManager
        {
                public void Disable(string modGUID)
                {
                        throw new NotImplementedException();
                }

                public void Enable(string modGUID)
                {
                        throw new NotImplementedException();
                }

                public List<string> GetModsOrdering()
                {
                        throw new NotImplementedException();
                }

                public bool Initialize(IModlRegistry registry)
                {
                        throw new NotImplementedException();
                }

                public bool IsDisabled(string modGUID)
                {
                        throw new NotImplementedException();
                }

                public bool IsEnabled(string modGUID)
                {
                        throw new NotImplementedException();
                }
        }
}
