using LCBM.API.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace LCBM.API.Event.Impl
{
        internal class LCBMEventBus : IEventBus
        {
                //类型，插件，监听器列表 三维字典
                readonly static Dictionary<Type, Dictionary<IModPlugin, List<Delegate>>> _subscribeLib = new Dictionary<Type, Dictionary<IModPlugin, List<Delegate>>>();

                //订阅事件
                public void Subscribe<T>(IModPlugin plugin, EventListener<T> listener) where T : IBaseEvent
                {
                        Type EventType = typeof(T);
                        //该类型事件首次被订阅
                        if (!_subscribeLib.TryGetValue(EventType, out Dictionary<IModPlugin, List<Delegate>> SubscribeInfo))
                        {
                                SubscribeInfo = new Dictionary<IModPlugin, List<Delegate>>
                                {
                                        { plugin, new List<Delegate>{listener} }
                                };
                                _subscribeLib.Add(EventType, SubscribeInfo);
                                return;
                        }
                        //插件首次订阅该事件
                        if (!SubscribeInfo.TryGetValue(plugin, out List<Delegate> ListenerList))
                        {
                                SubscribeInfo.Add(plugin, new List<Delegate> { listener });
                                return;
                        }
                        //直接添加
                        ListenerList.Add(listener);
                }


                public  void UnSubscribe<T>(IModPlugin plugin, EventListener<T> listener) where T : IBaseEvent
                {
                        Type EventType = typeof(T);

                        if (!_subscribeLib.TryGetValue(EventType, out Dictionary<IModPlugin, List<Delegate>> SubscribeInfo)) return;

                        if (!SubscribeInfo.TryGetValue(plugin, out List<Delegate> ListenerList)) return;

                        ListenerList.Remove(listener);
                }


                public  void Publish<T>(T evt) where T : IBaseEvent
                {
                        Type EventType = typeof(T);

                        if (!_subscribeLib.TryGetValue(EventType, out Dictionary<IModPlugin, List<Delegate>> SubscribeInfo)) return;

                        foreach (List<Delegate> ListenerList in SubscribeInfo.Values)
                        {
                                foreach (Delegate var in ListenerList)
                                {
                                        if (var is EventListener<T> listener)
                                        {
                                                listener(evt);
                                        }
                                }

                        }

                }




        }
}
