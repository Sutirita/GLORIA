using GLORIA.API;
using GLORIA.API.Core;
using GLORIA.API.Core.Buff;
using GLORIA.API.Entity;
using GLORIA.API.Event.SystemEvent;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GLORIA.Core.Buff
{
        //BUff的层数信息（对于不同层数的效果应由buff本身实现）
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

        //对全图单位的buff信息进行管理
        internal class BuffManager : IBuffManager//, MonoBehaviour
        {

                //[unit.id][buff.id]buffinfo
                private static readonly Dictionary<string, Dictionary<string, UnitBuffInfo>> _BuffLib = new Dictionary<string, Dictionary<string, UnitBuffInfo>>();

                //[unit.id][buff.id]List<IStatModifier>
                private readonly Dictionary<string, Dictionary<string, List<IStatModifier>>> _ModifierLib = new Dictionary<string, Dictionary<string, List<IStatModifier>>>();
                public bool Initialize()
                {
                        _BuffLib.Clear();
                        _ModifierLib.Clear();
                        return true;
                }

                public bool HasBuff(IUnit unit, string buffid)
                {
                        if (unit is null || string.IsNullOrEmpty(buffid)) return false;

                        if (!_BuffLib.ContainsKey(unit.Id)) return false;

                        if (!_BuffLib[unit.Id].ContainsKey(buffid)) return false;

                        return true;

                }

                public bool TryGetBuff(IUnit unit, string buffid, out IUnitBuff buff)
                {
                        buff = null;

                        if (!HasBuff(unit, buffid)) return false;

                        buff = _BuffLib[unit.Id][buffid].Buff;

                        return true;

                }

                public int GetBuffStack(IUnit unit, string buffid)
                {

                        if (!HasBuff(unit, buffid)) return -1;

                        UnitBuffInfo buffinfo = _BuffLib[unit.Id][buffid];

                        return buffinfo.CurrentStack;

                }

                public void AddBuff(IUnit unit, IUnitBuff buff, int stack = 1)
                {
                        if (unit is null || buff is null) return;

                        if (!buff.Stackable && stack != 1) stack = 1;

                        if (!_BuffLib.ContainsKey(unit.Id))
                        {
                                _BuffLib[unit.Id] = new Dictionary<string, UnitBuffInfo>();
                        }


                        if (!_BuffLib[unit.Id].ContainsKey(buff.Id))
                        {
                                _BuffLib[unit.Id].Add(buff.Id, new UnitBuffInfo(buff, stack));
                        }

                        if (!buff.Stackable)
                        {
                                if (buff is ITickableBuff tickableBuff)
                                {
                                        tickableBuff.RemainTime = tickableBuff.Duration;
                                }
                                return;
                        }
                        UnitBuffInfo info = _BuffLib[unit.Id][buff.Id];

                        if (info.CurrentStack == info.Buff.MaxStack) return;

                        info.CurrentStack += stack;

                        UpdateUnitBuffState(unit);

                        CalculateUnitStat(unit);

                        GLOBAL.EventBus.Publish<BuffAddEvent>(new BuffAddEvent(unit, buff));

                }

                public void RemoveBuff(IUnit unit, IUnitBuff buff, int stack = 1)
                {
                        if (!HasBuff(unit, buff.Id)) return;

                        if (!buff.Stackable)
                        {
                                _BuffLib[unit.Id].Remove(buff.Id);
                                return;
                        }

                        UnitBuffInfo buffinfo = _BuffLib[unit.Id][buff.Id];

                        if (buffinfo.CurrentStack > stack) buffinfo.CurrentStack -= stack;

                        if (buffinfo.CurrentStack == 0) _BuffLib[unit.Id].Remove(buff.Id);

                        UpdateUnitBuffState(unit);

                        CalculateUnitStat(unit);

                        if (_BuffLib[unit.Id].Keys.Count == 0) _BuffLib.Remove(unit.Id);

                        GLOBAL.EventBus.Publish<BuffRemoveEvent>(new BuffRemoveEvent(unit, buff));

                }



                internal void UpdateUnitBuffState(IUnit unit)
                {

                        Dictionary<string, List<IStatModifier>> dict = new Dictionary<string, List<IStatModifier>>();

                        foreach (string k in _BuffLib[unit.Id].Keys)
                        {
                                IUnitBuff buff = _BuffLib[unit.Id][k].Buff;
                                foreach (IStatModifier modifier in buff.StatModifiers)
                                {
                                        if (!dict.TryGetValue(modifier.Stat.Id, out var modifiers))
                                        {
                                                modifiers = new List<IStatModifier>();
                                                dict.Add(modifier.Stat.Id, modifiers);
                                        }
                                        modifiers.Add(modifier);
                                }
                        }

                        _ModifierLib[unit.Id] = dict;

                }


                internal void CalculateUnitStat(IUnit unit)
                {
                        //[Stat.Id]List<IStatModifier>
                        if (!_ModifierLib.TryGetValue(unit.Id, out Dictionary<string, List<IStatModifier>> dict)) return;

                        foreach (string statid in dict.Keys)
                        {
                                StatType type = new StatType(statid);
                                if (unit.TryGetStat(type, out float baseValue)) ;
                                float NewValue = Calculate(baseValue, dict[statid]);
                                unit.SetStat(type, NewValue);
                        }
                }

                internal float Calculate(float baseValue, IEnumerable<IStatModifier> modifiers)
                {
                        float add = 0f;
                        float multiply = 1f;
                        float finalAdd = 0f;
                        float finalMultiply = 1f;

                        foreach (var modifier in modifiers)
                        {
                                switch (modifier.Type)
                                {
                                        case ModifierOperation.Add:
                                                add += modifier.Value;
                                                break;

                                        case ModifierOperation.Multiply:
                                                multiply *= modifier.Value;
                                                break;

                                        case ModifierOperation.FinalAdd:
                                                finalAdd += modifier.Value;
                                                break;

                                        case ModifierOperation.FinalMultiply:
                                                finalMultiply *= modifier.Value;
                                                break;
                                }
                        }
                        return ((baseValue + add) * Math.Max(multiply, 0f) + finalAdd) * Math.Max(finalMultiply, 0f);
                }

                /*
                void FixedUpdate()
                {




                }

                */

        }
}
