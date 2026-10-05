using GLORIA.API.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Core
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
