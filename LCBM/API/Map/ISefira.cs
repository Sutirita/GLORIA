using LCBM.API.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LCBM.API.Mangement
{
        internal interface ISefira
        {
                Color SefiraColor { get; }

                IEnumerable<IAgent> Agents { get; }

                IEnumerable<IOfficer> Officers { get; }

                IEnumerable<ICreature> Creatures { get; }

                int OfficerBonusLevel { get; }

                bool IsOpen { get; }

        }
}
