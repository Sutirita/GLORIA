using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Core
{
        public readonly struct StatType
        {
                public string Id { get; }

                public StatType(string id)
                {
                        Id = id;
                }
        }
        public static class StatTypes
        {

             //   public static readonly StatType TimeMult = new StatType("Core.TimeMult");

                public static readonly StatType Health = new StatType("Core.Health");

                public static readonly StatType Mental = new StatType("Core.Worker.Mental");

                public static readonly StatType Movement = new StatType("Core.Movement");

                public static readonly StatType MovementMult = new StatType("Core.MovementMult");


                public static readonly StatType Rdefense = new StatType("Core.Defense.R");
                public static readonly StatType Wdefense = new StatType("Core.Defense.W");
                public static readonly StatType Bdefense = new StatType("Core.Defense.B");
                public static readonly StatType Pdefense = new StatType("Core.Defense.P");


                public static readonly StatType Rstat = new StatType("Core.Worker.RWBPstat.R");
                public static readonly StatType Wstat = new StatType("Core.Worker.RWBPstat.W");
                public static readonly StatType Bstat = new StatType("Core.Worker.RWBPstat.B");
                public static readonly StatType Pstat = new StatType("Core.Worker.RWBPstat.P");


            
                public static readonly StatType WorkProb = new StatType("Core.Agent.WorkProb");

                public static readonly StatType WorkSpeed = new StatType("Core.Agent.WorkSpeed");

                public static readonly StatType AttackSpeed = new StatType("Core.Worker.AttackSpeed");

                public static readonly StatType MoveSpeed = new StatType("Core.Worker.MoveSpeed");


        }

}
