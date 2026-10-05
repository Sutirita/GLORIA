using GLORIA.API;
using GLORIA.API.Core.Buff;
using GLORIA.API.Entity;
using GLORIA.API.Event.SystemEvent;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GLORIA.Core.Buff
{
        internal struct UnitBuffInfo
        {
                public int CurrentStack;

                public readonly IUnitBuff Buff;

                public UnitBuffInfo(IUnitBuff buff, int stack)
                {
                        Buff = buff;
                        CurrentStack = stack;
                }
        }


        internal class BuffManager :MonoBehaviour,IBuffManager
        {
                private static readonly Dictionary<IUnit, Dictionary<string, UnitBuffInfo>> _BuffDict = new Dictionary<IUnit, Dictionary<string, UnitBuffInfo>>();


                private static readonly List<ITickableBuff> tickableBuffs = new List<ITickableBuff>();


                public bool HasBuff(IUnit unit, string buffid)
                {
                        if (unit is null || string.IsNullOrEmpty(buffid)) return false;

                        if (!_BuffDict.ContainsKey(unit)) return false;

                        if (!_BuffDict[unit].ContainsKey(buffid)) return false;

                        return true;

                }

                public bool TryGetBuff(IUnit unit, string buffid, out IUnitBuff buff)
                {
                        buff = null;

                        if (!HasBuff(unit, buffid)) return false;

                        buff = _BuffDict[unit][buffid].Buff;

                        return true;

                }


                public int GetBuffStack(IUnit unit, string buffid)
                {

                        if (!HasBuff(unit, buffid)) return -1;

                        UnitBuffInfo buffinfo = _BuffDict[unit][buffid];

                        return buffinfo.CurrentStack;

                }

                public void AddBuff(IUnit unit, IUnitBuff buff, int stack = 1)
                {
                        if (unit is null || buff is null) return;

                        if (!buff.Stackable && stack != 1) stack = 1;

                        if (!_BuffDict.ContainsKey(unit))
                        {
                                _BuffDict[unit] = new Dictionary<string, UnitBuffInfo>();
                        }


                        if (!_BuffDict[unit].ContainsKey(buff.Id))
                        {
                                _BuffDict[unit].Add(buff.Id, new UnitBuffInfo(buff, stack));
                        }

                        if (!buff.Stackable)
                        {
                                if (buff is ITickableBuff tickableBuff)
                                {
                                        tickableBuff.RemainTime = tickableBuff.Duration;
                                }
                                return;
                        }
                        UnitBuffInfo info = _BuffDict[unit][buff.Id];

                        if (info.CurrentStack == info.Buff.MaxStack) return;

                        info.CurrentStack += 1;

                        GLOBAL.EventBus.Publish<BuffAddEvent>(new BuffAddEvent(unit, buff));

                }

                public void RemoveBuff(IUnit unit, IUnitBuff buff, int stack = 1)
                {
                        if (!HasBuff(unit, buff.Id)) return;

                        if (!buff.Stackable) _BuffDict[unit].Remove(buff.Id);

                        UnitBuffInfo buffinfo = _BuffDict[unit][buff.Id];

                        if (buffinfo.CurrentStack > stack) buffinfo.CurrentStack -= stack;

                        _BuffDict[unit].Remove(buff.Id);

                        if (_BuffDict[unit].Keys.Count > 0) return;

                        _BuffDict.Remove(unit);



                        GLOBAL.EventBus.Publish<BuffRemoveEvent>(new BuffRemoveEvent(unit,buff));

                }



     


        }
}
