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

                IEnumerable<IAgentUnit> Agents { get; }

                IAgentUnit GetAgent(string id);

                IEnumerable<IOfficerUnit> Officers { get; }

                IOfficerUnit GetOfficer(string id);

                IEnumerable<ICreatureUnit> Creatures { get; }

                ICreatureUnit GetCreature(string id);

                int OfficerBonusLevel { get; }

                bool IsOpen { get; }

        }
}
