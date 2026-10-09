using GLORIA.API.Entity.Creature;
using GLORIA.API.Entity.Worker;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GLORIA.API.Mangement
{
        internal interface ISefira
        {
                Color SefiraColor { get; }

                IEnumerable<IAgent> Agents { get; }

                IAgent GetAgent(string id);

                IEnumerable<IOfficer> Officers { get; }

                IOfficer GetOfficer(string id);

                IEnumerable<ICreature> Creatures { get; }

                ICreature GetCreature(string id);

                int OfficerBonusLevel { get; }

                bool IsOpen { get; }

        }
}
