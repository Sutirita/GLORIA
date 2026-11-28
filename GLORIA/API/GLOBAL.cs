using GLORIA.API.Core.Buff;
using GLORIA.API.Core.Mod;
using GLORIA.API.Event;

namespace GLORIA.API
{
        public static class GLOBAL
        {


                public static IBuffManager BuffManager { get; private set; }

                public static IEventBus EventBus { get; private set; }

                public static IModlRegistry ModlRegistry { get; private set; }

                public static bool Initialize()
                {
                        bool flag = true;


                        flag = flag && EventBus.Initialize();

                        flag = flag && ModlRegistry.Initialize();


                        flag = flag && BuffManager.Initialize();

                        return flag;
                }



        }






}
