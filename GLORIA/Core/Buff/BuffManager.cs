using GLORIA.API;
using GLORIA.API.Core;
using GLORIA.API.Core.Buff;
using GLORIA.API.Entity;
using GLORIA.API.Event.SystemEvent;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

namespace GLORIA.Core.Buff
{


        //对全图单位的buff信息进行管理
        internal class BuffManager : IBuffManager//, MonoBehaviour
        {
                //三维字典[unit.id:buff.id:buff]，存储全图单位BUff数据
                private readonly Dictionary<string, Dictionary<string, IUnitBuff>> unitsBuffInfo;

                //三维字典[unit.id:attr.id:AttrModifier]，存储全图单位增益缓存
                private readonly Dictionary<string, Dictionary<string, List<IAttrModifier>>> unitsBonusInfo;

                public bool Initialize()
                {
                        unitsBuffInfo.Clear();
                        unitsBonusInfo.Clear();
                        return true;
                }
                public BuffManager()
                {
                        unitsBuffInfo = new Dictionary<string, Dictionary<string, IUnitBuff>>();

                        unitsBonusInfo = new Dictionary<string, Dictionary<string, List<IAttrModifier>>>();
                }

                public bool IsBuffedUnit(IUnit unit)
                {
                        if (unit is null) return false;

                        if (!unitsBuffInfo.ContainsKey(unit.Id)) return false;

                        return (unitsBuffInfo[unit.Id].Keys.Count > 0);

                }

                public bool HasBuff(IUnit unit, string buffid)
                {
                        if (unit is null || string.IsNullOrEmpty(buffid)) return false;

                        if (!IsBuffedUnit(unit)) return false;

                        if (!unitsBuffInfo[unit.Id].ContainsKey(buffid)) return false;

                        return true;
                }

                public bool TryGetBuff(IUnit unit, string buffid, out IUnitBuff buff)
                {
                        buff = null;

                        if (!HasBuff(unit, buffid))
                        {
                                Logger.Error($"Can not find buff:{buff.Id} in unit:{unit.Id}.");

                                return false;
                        }

                        buff = unitsBuffInfo[unit.Id][buffid];

                        return true;

                }

                public int GetBuffStack(IUnit unit, string buffid)
                {
                        if (!TryGetBuff(unit, buffid, out IUnitBuff buff)) return -1;

                        if (buff is IStackableBuff stackable) return stackable.CurrentStack;

                        Logger.Error($"The buff {buff.Id} is unstackable.");

                        return -1;

                }




                public void AddBuff(IUnit unit, IUnitBuff buff, int stack = 1)
                {
                        if (unit is null || buff is null) return;

                        if (!IsBuffedUnit(unit)) unitsBuffInfo.Add(unit.Id, new Dictionary<string, IUnitBuff>());

                        if (!HasBuff(unit, buff.Id)) unitsBuffInfo[unit.Id].Add(buff.Id, buff);

                        IUnitBuff oldbuff = unitsBuffInfo[unit.Id][buff.Id];

                        if (oldbuff is IStackableBuff stackable)
                        {
                                stackable.SetStack(Math.Max(stackable.CurrentStack + stack, stackable.MaxStack));
                        }

                        if (oldbuff is ITickableBuff tickable)
                        {
                                tickable.RemainTime = tickable.Duration;
                        }

                        RebuildUnitModifiers(unit);

                        GLOBAL.EventBus.Publish<BuffAddEvent>(new BuffAddEvent(unit, buff));

                }


                public void RemoveBuff(IUnit unit, string buffId, int stack = 1)
                {
                        if (unit is null || string.IsNullOrEmpty(buffId)) return;

                        if (!IsBuffedUnit(unit)) return;

                        if (!HasBuff(unit, buffId)) return;

                        IUnitBuff oldbuff = unitsBuffInfo[unit.Id][buffId];

                        if (oldbuff is IStackableBuff stackable)
                        {
                                if (stackable.CurrentStack <= stack)
                                {
                                        unitsBuffInfo[unit.Id].Remove(buffId);
                                }
                                else
                                {
                                        stackable.SetStack(stackable.CurrentStack - stack);
                                }
                        }
                        else
                        {
                                unitsBuffInfo[unit.Id].Remove(buffId);
                        }
                        RebuildUnitModifiers(unit);

                        if (!IsBuffedUnit(unit)) unitsBuffInfo.Remove(unit.Id);

                        GLOBAL.EventBus.Publish<BuffRemoveEvent>(new BuffRemoveEvent(unit, buffId, stack));

                }


                private void RebuildUnitModifiers(IUnit unit)
                {
                        if(unit is null) return;

                        if (!IsBuffedUnit(unit)) return; 

                        Dictionary<string, List<IAttrModifier>> Bonus = new Dictionary<string, List<IAttrModifier>>();

                        unitsBonusInfo[unit.Id] = Bonus;

                        foreach(IUnitBuff buff in unitsBuffInfo[unit.Id].Values)
                        {

                                foreach(IAttrModifier modifier in buff.StatModifiers)
                                {

                                        if (!Bonus.ContainsKey(modifier.Stat.Value)) Bonus.Add(modifier.Stat.Value, new List<IAttrModifier>());

                                        Bonus[modifier.Stat.Value].Add(modifier);

                                }

                        }

                }



                internal float Calculate(float baseValue, IEnumerable<IAttrModifier> modifiers)
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


                public bool TryGetAttrBonus(IUnit unit, AttrType type, out IEnumerable<IAttrModifier> modifiers)
                {
                        modifiers = new List<IAttrModifier>();

                        if (!unit.Attrs.ContainsKey(type.Value)) return false;

                        modifiers = unitsBonusInfo[unit.Id][type.Value];

                        return true;
                }



                public bool TryGetAttrWithBonus(IUnit unit, AttrType type, out float Value)
                {

                        Value = 0;

                        if (!unit.Attrs.TryGetValue(type.Value, out float baseValue)) return false;

                        Value = Calculate(baseValue, unitsBonusInfo[unit.Id][type.Value]);

                        return true;
                }



                
        }
}
