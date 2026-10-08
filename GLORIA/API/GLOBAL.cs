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






}
