using System;
using System.Collections.Generic;
using GLORIA.API.Event;
using GLORIA.Core;
using GLORIA.API.Core.Mod;

namespace GLORIA.Event
{
        internal class MainEventBus : IEventBus
        {
                //类型，插件，监听器列表 三维字典
                private readonly static Dictionary<Type, Dictionary<IMod, List<Delegate>>> _subscribeLib = new Dictionary<Type, Dictionary<IMod, List<Delegate>>>();


                public bool Initialize()
                {
                        _subscribeLib.Clear();
                        return true;
                }



                //订阅事件
                public void Subscribe<T>(IMod plugin, EventListener<T> listener) where T : IBaseEvent
                {
                        Type EventType = typeof(T);
                        //该类型事件首次被订阅
                        if (!_subscribeLib.TryGetValue(EventType, out Dictionary<IMod, List<Delegate>> SubscribeInfo))
                        {
                                SubscribeInfo = new Dictionary<IMod, List<Delegate>>
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

                        //不重复添加
                        if (ListenerList.Contains(listener)) {
                                Logger.Warning($"Listener:{listener} is already exsist,you need to unsubscribe it first.");
                                return;
                        }
                        //添加
                        ListenerList.Add(listener);
                }


                public void Unsubscribe<T>(IMod plugin, EventListener<T> listener) where T : IBaseEvent
                {
                        Type EventType = typeof(T);

                        if (!_subscribeLib.TryGetValue(EventType, out Dictionary<IMod, List<Delegate>> SubscribeInfo)) return;

                        if (!SubscribeInfo.TryGetValue(plugin, out List<Delegate> ListenerList)) return;

                        ListenerList.Remove(listener);

                        if (ListenerList.Count == 0) SubscribeInfo.Remove(plugin);

                        if (SubscribeInfo.Count == 0) _subscribeLib.Remove(EventType);
                }


                public void Publish<T>(T evt) where T : IBaseEvent
                {
                        Type EventType = typeof(T);

                        if (!_subscribeLib.TryGetValue(EventType, out Dictionary<IMod, List<Delegate>> SubscribeInfo)) return;

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
