using System;
using System.Collections.Generic;
using LCBM.API.Event;
using LCBM.API.Core;

namespace LCBM.Event
{
        public class LCBMEventBus : IEventBus
        {
                //类型，插件，监听器列表 三维字典
                private readonly static Dictionary<Type, Dictionary<IModPlugin, List<Delegate>>> _subscribeLib = new Dictionary<Type, Dictionary<IModPlugin, List<Delegate>>>();

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


                public void Unsubscribe<T>(IModPlugin plugin, EventListener<T> listener) where T : IBaseEvent
                {
                        Type EventType = typeof(T);

                        if (!_subscribeLib.TryGetValue(EventType, out Dictionary<IModPlugin, List<Delegate>> SubscribeInfo)) return;

                        if (!SubscribeInfo.TryGetValue(plugin, out List<Delegate> ListenerList)) return;

                        ListenerList.Remove(listener);

                        if (ListenerList.Count == 0) SubscribeInfo.Remove(plugin);

                        if (SubscribeInfo.Count == 0) _subscribeLib.Remove(EventType);
                }


                public void Publish<T>(T evt) where T : IBaseEvent
                {
                        Type EventType = typeof(T);

                        if (!_subscribeLib.TryGetValue(EventType, out Dictionary<IModPlugin, List<Delegate>> SubscribeInfo)) return;

                        foreach (List<Delegate> ListenerList in SubscribeInfo.Values)
                        {
                                foreach (Delegate var in ListenerList)
                                {
                                        if (!(var is EventListener<T> listener)) continue;
                                        listener(evt);
                                }
                        }
                }

        }
}
