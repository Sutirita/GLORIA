using GLORIA.API.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Entity
{
        public interface IUnit
        {
                string Id {  get; }

                bool TryGetStat(StatType type,out float value );

                void SetStat(StatType type,float value);
        }
}
