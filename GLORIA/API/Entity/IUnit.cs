using GLORIA.API.Core;
using GLORIA.API.Core.Buff;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Entity
{
        public interface IUnit
        {
                string Id { get; }

                Dictionary<string, float> Attrs { get; }

              
        }
}
