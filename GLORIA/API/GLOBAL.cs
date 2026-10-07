using GLORIA.API.Core.Buff;
using GLORIA.API.Core.Mod;
using GLORIA.API.Event;
using GLORIA.Core.Buff;
using GLORIA.Core.Mod;
using GLORIA.Event;

namespace GLORIA.API
{
        public static class GLOBAL
        {
                internal static IEventBus _eventBus;

                internal static IModlRegistry _modlRegistry;

                internal static IBuffManager _buffManager;

                
                internal static bool Initialize()
                {
                        _eventBus = new MainEventBus();

                        _modlRegistry = new ModRegistry();

                        _buffManager = new BuffManager();
                        
                        return true;
                }






                public static IBuffManager BuffManager => _buffManager;

                public static IEventBus EventBus => _eventBus;

                public static IModlRegistry ModlRegistry => _modlRegistry;


        }





        public readonly struct StatId
        {
                public string Value { get; }

                public StatId(string value)
                {
                        Value = value;
                }
        }




        public static class Stats
        {
                public static readonly StatId HP = new StatId("Core.HP");

                public static readonly StatId Mental = new StatId("Core.Mental");

                public static readonly StatId Movement = new StatId("Core.Movement");

                public static readonly StatId MovementMult = new StatId("Core.MovementMult");

                public static readonly StatId AttackSpeed = new StatId("Core.AttackSpeed");

                public static readonly StatId DamageMult = new StatId("Core.Damage.R");


                public static readonly StatId Rdefense = new StatId("Core.Defense.R");
                public static readonly StatId Wdefense = new StatId("Core.Defense.W");
                public static readonly StatId Bdefense = new StatId("Core.Defense.B");
                public static readonly StatId Pdefense = new StatId("Core.Defense.P");


                public static readonly StatId Rstat = new StatId("Core.Agent.RWBPstat.R");
                public static readonly StatId Wstat = new StatId("Core.Agent.RWBPstat.W");
                public static readonly StatId Bstat = new StatId("Core.Agent.RWBPstat.B");
                public static readonly StatId Pstat = new StatId("Core.Agent.RWBPstat.P");

                public static readonly StatId WorkSpeed = new StatId("Core.Agent.WorkSpeed");

                public static readonly StatId WorkProb = new StatId("Core.Agent.WorkProb");


        }

}
