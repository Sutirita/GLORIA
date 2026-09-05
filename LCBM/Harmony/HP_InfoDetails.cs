using CommandWindow;
using CreatureInfo;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
using LCBM.Core;

namespace LCBM.Harmony
{

        //unuse
        class HP_InfoDetails
        {

                //伤害抗性显示小数
                [HarmonyPrefix]
                [HarmonyPatch(typeof(UIUtil))]
                [HarmonyPatch("DefenseSetFactor", MethodType.Normal)]
                [HarmonyPatch(new Type[] { typeof(DefenseInfo), typeof(Text[]), typeof(bool) })]
                public static bool HP_DefenseSetFactor(DefenseInfo defenseInfo, Text[] text, bool bracket = false)
                {
                        int acc = LCBMConfig.Precision_Defense.Value;
                        string format = bracket ? "({0:F" + acc + "})" : "{0:F" + acc + "}";

                        text[0].text = string.Format(format, defenseInfo.R);
                        text[1].text = string.Format(format, defenseInfo.W);
                        text[2].text = string.Format(format, defenseInfo.B);
                        text[3].text = string.Format(format, defenseInfo.P);
                        return false;
                }





                //武器攻速，攻击距离显示数值
                [HarmonyPrefix]
                [HarmonyPatch(typeof(InventoryItemDescGetter))]
                [HarmonyPatch("GetWeaponDesc", MethodType.Normal)]
                [HarmonyPatch(new Type[] { typeof(EquipmentTypeInfo), typeof(string), typeof(string) }, new ArgumentType[] { ArgumentType.Normal, ArgumentType.Out, ArgumentType.Out })]

                public static bool HP_GetWeaponATKSpeedDesc(EquipmentTypeInfo model, out string attackSpeed, out string attackRange)
                {
                        int acc = 2;
                        float attackSpeed2 = model.attackSpeed;
                        float range = model.range;


                        string _format = "[{0:F" + acc + "}]";

                        int num;
                        if ((double)attackSpeed2 < 1.1)
                        {
                                num = 0;
                        }
                        else if (attackSpeed2 < 2f)
                        {
                                num = 1;
                        }
                        else if (attackSpeed2 < 3f)
                        {
                                num = 2;
                        }
                        else if (attackSpeed2 < 5f)
                        {
                                num = 3;
                        }
                        else
                        {
                                num = 4;
                        }
                        int num2;
                        if (range < 3f)
                        {
                                num2 = 4;
                        }
                        else if (range < 4f)
                        {
                                num2 = 3;
                        }
                        else if (range < 10f)
                        {
                                num2 = 2;
                        }
                        else if (range < 20f)
                        {
                                num2 = 1;
                        }
                        else
                        {
                                num2 = 0;
                        }

                        attackSpeed = InventoryItemDescGetter.GetText(InventoryItemDescGetter.AttackSpeedKey[num]);

                        attackRange = InventoryItemDescGetter.GetText(InventoryItemDescGetter.AttackRangeKey[num2]);

                        attackSpeed += string.Format(_format, model.attackSpeed);

                        attackRange += string.Format(_format, model.range);

                        return false;
                }




                //优化武器信息显示

                [HarmonyPostfix]
                [HarmonyPatch(typeof(InventoryWeaponSlot))]
                [HarmonyPatch("UpdateUI", MethodType.Normal)]

                public static void HP_UpdateUI(InventoryWeaponSlot __instance)
                {
                        __instance.DamageRange.text = __instance.DamageRange.text.Replace("-", "~");
                        __instance.Range.text = __instance.DamageRange.text.Replace("[", "\n[");
                }






                //图鉴工作显示详细成功率
                [HarmonyPrefix, HarmonyPatch(typeof(CreatureInfoWorkSlot), "SetWorkSuccess", new Type[] { })]
                public static bool HP_CreatureInfoWorkSlot_SWS(CreatureInfoWorkSlot __instance)
                {
                        UnityEngine.UI.Text[] levelSuccessPercentage = (UnityEngine.UI.Text[])Traverse.Create(__instance).Field("levelSuccessPercentage").GetValue();
                        RwbpType _type = (RwbpType)Traverse.Create(__instance).Field("_type").GetValue();
                        for (int i = 0; i < levelSuccessPercentage.Length; i++)
                        {
                                UnityEngine.UI.Text text = levelSuccessPercentage[i];
                                float num = new CreatureInfoController().MetaInfo.workProbTable.GetWorkProb(_type, i + 1);
                                string percentText = UICommonTextConverter.GetPercentText(num);
                                text.text = percentText;
                        }
                        return false;
                }


                //工作成功率小数格式
                [HarmonyPrefix, HarmonyPatch(typeof(UICommonTextConverter), "GetPercentText", new Type[] { typeof(float) })]
                public static bool HP_UICommonTextConverter(ref string __result, float rate)
                {
                        int acc = LCBMConfig.Precision_WorkPorb.Value;
                        __result = string.Format("{0:F" + acc + "}", (rate * 100f)) + "%";

                        return false;

                }


                //工作时间显示小数
                [HarmonyPostfix, HarmonyPatch(typeof(WorkData), "CheckCurrentSkill")]

                public static void HP_WDCheckCurrentSkill(WorkData __instance)
                {

                        SkillTypeInfo current = Traverse.Create(__instance).Field<SkillTypeInfo>("_current").Value;
                        if (current is null)
                        {
                                return;
                        }
                        AgentModel currentAgent = Traverse.Create(__instance).Field<AgentModel>("_currentAgent").Value;
                        if (currentAgent is null)
                        {
                                return;
                        }
                        CreatureModel currentCreature = Traverse.Create(__instance).Field<CreatureModel>("_currentCreature").Value;
                        if (currentCreature is null)
                        {
                                return;
                        }
                        int acc = LCBMConfig.Precision_WorkPorb.Value;
                        float ws = (float)currentCreature.metaInfo.feelingStateCubeBounds.GetLastBound() / (currentCreature.GetCubeSpeed() * (1f + (float)(currentCreature.GetObserveBonusSpeed() + currentAgent.workSpeed) / 100f));
                        __instance.WorkSpeed.text = string.Format("{0:F" + acc + "}s", ws);

                }





                //---显示伤害效果的小数部分---


                //伤害特效显示小数
                [HarmonyPrefix, HarmonyPatch(typeof(UnitModel), "MakeDamageEffect")]
                public static bool HP_MakeDamageEffect(RwbpType type, float value, DefenseInfo.Type defense, UnitModel __instance)
                {
                        if (ResearchDataModel.instance.IsUpgradedAbility("damage_text") || (GlobalGameManager.instance.gameMode == GameMode.TUTORIAL && GlobalGameManager.instance.tutorialStep > 1))
                        {
                                MovableObjectNode mov = __instance.GetMovableNode();

                                DamageEffect effect = DamageEffect.Invoker(mov);

                                if (defense == DefenseInfo.Type.NONE)
                                {
                                        effect.DefenseTypeInner.gameObject.SetActive(false);
                                        effect.DamageCount.rectTransform.anchoredPosition = new Vector2(-83f, effect.DamageCount.rectTransform.anchoredPosition.y);
                                        effect.Icon.rectTransform.anchoredPosition = new Vector2(5f, effect.Icon.rectTransform.anchoredPosition.y);
                                        effect.IconOut.rectTransform.anchoredPosition = effect.Icon.rectTransform.anchoredPosition;
                                        effect.DefenseTypeText.enabled = false;
                                }
                                else
                                {
                                        if (defense == DefenseInfo.Type.IMMUNE)
                                        {
                                                effect.DamageCount.gameObject.SetActive(false);
                                        }
                                        else
                                        {
                                                effect.DamageCount.gameObject.SetActive(true);
                                        }

                                        effect.DefenseTypeInner.gameObject.SetActive(true);

                                        int num = 0;

                                        switch (defense)
                                        {
                                                case DefenseInfo.Type.SUPER_WEAKNESS:
                                                        num = 0;
                                                        break;
                                                case DefenseInfo.Type.WEAKNESS:
                                                        num = 1;
                                                        break;
                                                case DefenseInfo.Type.RESISTANCE:
                                                        num = 3;
                                                        break;
                                                case DefenseInfo.Type.IMMUNE:
                                                        num = 2;
                                                        break;
                                                case DefenseInfo.Type.ENDURE:
                                                        num = 4;
                                                        break;
                                        }
                                        effect.DefenseTypeText.sprite = effect.DamageTextImage[num];
                                        effect.DefenseTypeInner.sprite = effect.DamageFontTexture[num];
                                        effect.DefenseTypeText.SetNativeSize();
                                        effect.DefenseTypeInner.SetNativeSize();
                                }

                                UIColorManager.instance.GetRWBPTypeColor(type, out Color white, out Color white2);
                                Sprite DamageIconSprite = effect.DamageIcon[(int)type];
                                Sprite DamageIconOutSprite = effect.DamageIconOut[(int)type];
                                if (__instance != null)
                                {
                                        int acc = LCBMConfig.Precision_Damage.Value;
                                        if (type == RwbpType.P && __instance is WorkerModel)
                                        {
                                                effect.DamageCount.text = string.Format("{0:F" + acc + "}", value);
                                        }
                                        else
                                        {
                                                effect.DamageCount.text = string.Format("{0:F" + acc + "}", value);
                                        }
                                }
                                effect.Icon.sprite = DamageIconSprite;
                                effect.IconOut.sprite = DamageIconOutSprite;
                                Graphic frame = effect.Frame;
                                Color color = white;
                                effect.DamageCount.color = color;
                                effect.DamageContext.color = color;
                                frame.color = color;
                                Graphic fill = effect.Fill;
                                color = white2;
                                effect.DamageCountOutline.effectColor = color;
                                fill.color = color;
                                effect.IconOut.color = white2;
                                effect.DefenseTypeText.color = Color.white;
                                effect.DefenseTypeInner.color = white2;

                                Vector2 offset = Vector2.zero;
                                switch (type)
                                {
                                        case RwbpType.N:
                                                effect.IconOut.rectTransform.anchoredPosition = effect.Icon.rectTransform.anchoredPosition;
                                                break;
                                        case RwbpType.R:
                                                offset = new Vector2(-2.5f, 9.6f);
                                                break;
                                        case RwbpType.W:
                                                offset = new Vector2(-3f, 6.8f);
                                                break;
                                        case RwbpType.B:
                                                offset = new Vector2(-3.1f, 0f);
                                                break;
                                        case RwbpType.P:
                                                offset = new Vector2(-3.1f, 0f);
                                                break;
                                        default:
                                                offset = new Vector2(-2.5f, 9.6f);
                                                break;
                                }
                                effect.IconOut.rectTransform.anchoredPosition = effect.Icon.rectTransform.anchoredPosition + offset;
                                effect.Icon.color = Color.white;
                                effect.Dettach();
                                effect.transform.localPosition = new Vector3(effect.transform.localPosition.x + UnityEngine.Random.Range(-0.5f, 0.5f), effect.transform.localPosition.y, effect.transform.localPosition.z);
                        }
                        return false;
                }


                //异想体单位
                [HarmonyPrefix, HarmonyPatch(typeof(CreatureModel), "TakeDamage")]
                public static bool HP_CreatureModelTkDmg_Pre(UnitModel actor, DamageInfo dmg, CreatureModel __instance)
                {
                        dmg = dmg.Copy();

                        if (!__instance.script.CanTakeDamage(actor, dmg))
                        {
                                return false;
                        }
                        __instance.Unit.model.Equipment.armor?.OnTakeDamage(actor, ref dmg);
                        __instance.Unit.model.Equipment.weapon?.OnTakeDamage(actor, ref dmg);
                        __instance.Unit.model.Equipment.gifts?.OnTakeDamage(actor, ref dmg);

                        float DamageNumber = 0f;

                        float DamageRate = 1f;

                        if (actor != null)
                        {
                                DamageRate = UnitModel.GetDmgMultiplierByEgoLevel(actor.GetAttackLevel(), __instance.GetDefenseLevel());
                        }
                        DamageRate *= __instance.GetBufDamageMultiplier(actor, dmg);
                        DamageRate *= __instance.script.GetDamageFactor(actor, dmg);
                        if (dmg.type == RwbpType.R || dmg.type == RwbpType.W)
                        {
                                DamageNumber = dmg.GetDamageWithDefenseInfo(__instance.defense) * DamageRate;
                        }
                        else if (dmg.type == RwbpType.B)
                        {
                                DamageNumber = dmg.GetDamageWithDefenseInfo(__instance.defense) * DamageRate;
                        }
                        else if (dmg.type == RwbpType.P)
                        {
                                DamageNumber = dmg.GetDamageWithDefenseInfo(__instance.defense) * DamageRate;
                        }
                        else if (dmg.type == RwbpType.N)
                        {
                                DamageNumber = dmg.GetDamageWithDefenseInfo(__instance.defense) * DamageRate;
                        }
                        if (__instance.hp > 0f)
                        {
                                if (DamageNumber >= 0f)
                                {
                                        __instance.hp -= DamageNumber;
                                        __instance.MakeDamageEffect(dmg.type, DamageNumber, __instance.defense.GetDefenseType(dmg.type));
                                        if (dmg.type == RwbpType.R || dmg.type == RwbpType.B || DamageNumber > 1f)
                                        {
                                                __instance.MakeSpatteredBlood();
                                        }
                                        __instance.script.OnTakeDamage(actor, dmg, DamageNumber);
                                }
                                else if (DamageNumber < 0f)
                                {
                                        float heal = -DamageNumber;
                                        float lostHp = (float)__instance.maxHp - __instance.hp;
                                        if (lostHp >= heal)
                                        {
                                                __instance.hp += heal;
                                        }
                                        else
                                        {
                                                __instance.hp = (float)__instance.maxHp;
                                        }
                                        GameObject gameObject = Prefab.LoadPrefab("Effect/RecoverHP");
                                        gameObject.transform.SetParent(__instance.Unit.animTarget.transform);
                                        gameObject.transform.localPosition = Vector3.zero;
                                        gameObject.transform.localScale = Vector3.one;
                                        gameObject.transform.localRotation = Quaternion.identity;
                                }
                        }
                        __instance.hp = Mathf.Clamp(__instance.hp, 0f, (float)__instance.maxHp);

                        if (__instance.state == CreatureState.ESCAPE && __instance.hp <= 0f)
                        {
                                __instance.Suppressed();
                        }
                        __instance.Unit.model.Equipment.armor?.OnTakeDamage_After(DamageNumber, dmg.type);
                        __instance.Unit.model.Equipment.weapon?.OnTakeDamage_After(DamageNumber, dmg.type);
                        __instance.Unit.model.Equipment.gifts?.OnTakeDamage_After(DamageNumber, dmg.type);


                        return false;

                }


                //职员单位
                [HarmonyPrefix, HarmonyPatch(typeof(WorkerModel), "TakeDamage")]
                public static bool HP_WorkerModelTkDmg_Pre(UnitModel actor, DamageInfo dmg, WorkerModel __instance)
                {
                        dmg = dmg.Copy();

                        List<BarrierBuf> barrierBufList = (List<BarrierBuf>)Traverse.Create(__instance).Field("_barrierBufList").GetValue();


                        if (__instance.invincible && !__instance.HasUnitBuf(UnitBufType.OTHER_WORLD_PORTRAIT_VICTIM))
                        {
                                return false;
                        }
                        if (__instance.IsDead())
                        {
                                return false;
                        }

                        __instance.Equipment.armor?.OnTakeDamage(actor, ref dmg);
                        __instance.Equipment.weapon?.OnTakeDamage(actor, ref dmg);
                        __instance.Equipment.gifts?.OnTakeDamage(actor, ref dmg);

                        float DamageMultiplier = 1f;

                        if (actor != null)
                        {
                                DamageMultiplier *= UnitModel.GetDmgMultiplierByEgoLevel(actor.GetAttackLevel(), __instance.GetDefenseLevel());
                        }
                        DamageMultiplier *= __instance.GetBufDamageMultiplier(actor, dmg);

                        float DamageValue = dmg.GetDamageWithDefenseInfo(__instance.defense);

                        DamageValue *= DamageMultiplier;

                        if (dmg.type == RwbpType.R)
                        {
                                foreach (BarrierBuf barrierBuf in barrierBufList.ToArray())
                                {
                                        DamageValue = barrierBuf.UseBarrier(RwbpType.R, DamageValue);
                                }

                                __instance.hp -= DamageValue;

                                __instance.Equipment.kitCreature?.script.kitEvent.OnTakeDamagePhysical(__instance, DamageValue);

                                __instance.MakeDamageEffect(RwbpType.R, DamageValue, __instance.defense.GetDefenseType(RwbpType.R));

                                float num3 = (float)__instance.maxHp;

                                if (__instance.maxHp > 0f && UnityEngine.Random.value < DamageValue * 2f / num3)
                                {
                                        __instance.MakeSpatteredBlood();
                                }
                        }
                        else if (dmg.type == RwbpType.W)
                        {
                                foreach (BarrierBuf barrierBuf2 in barrierBufList.ToArray())
                                {
                                        DamageValue = barrierBuf2.UseBarrier(RwbpType.W, DamageValue);
                                }
                                if (__instance.CannotControll() && __instance.unconAction is Uncontrollable_RedShoes)
                                {
                                        __instance.hp -= DamageValue;
                                        __instance.Equipment.kitCreature?.script.kitEvent.OnTakeDamagePhysical(__instance, DamageValue);
                                }
                                else if (__instance.IsPanic() && !(__instance is OfficerModel) && actor is WorkerModel model1 && !model1.CannotControll() && !model1.IsPanic())
                                {
                                        __instance.mental += DamageValue;
                                }
                                else if (__instance is OfficerModel)
                                {
                                        __instance.mental -= DamageValue;
                                        __instance.hp -= DamageValue;
                                }
                                else
                                {
                                        __instance.mental -= DamageValue;
                                        __instance.Equipment.kitCreature?.script.kitEvent.OnTakeDamageMental(__instance, DamageValue);
                                }
                                __instance.MakeDamageEffect(RwbpType.W, DamageValue, __instance.defense.GetDefenseType(RwbpType.W));
                        }
                        else if (dmg.type == RwbpType.B)
                        {
                                foreach (BarrierBuf barrierBuf3 in barrierBufList.ToArray())
                                {
                                        DamageValue = barrierBuf3.UseBarrier(RwbpType.B, DamageValue);
                                }
                                __instance.hp -= DamageValue;

                                if (__instance.IsPanic() && !(__instance is OfficerModel) && actor is WorkerModel model1 && !model1.CannotControll() && !model1.IsPanic())
                                {
                                        __instance.mental += DamageValue;
                                }
                                else
                                {
                                        __instance.mental -= DamageValue;
                                        __instance.Equipment.kitCreature?.script.kitEvent.OnTakeDamageMental(__instance, DamageValue);
                                }
                                __instance.MakeDamageEffect(RwbpType.B, DamageValue, __instance.defense.GetDefenseType(RwbpType.B));
                                float num5 = (float)__instance.maxHp;
                                if (num5 > 0f && UnityEngine.Random.value < DamageValue * 2f / num5)
                                {
                                        __instance.MakeSpatteredBlood();
                                }
                        }
                        else if (dmg.type == RwbpType.P)
                        {
                                float num6 = DamageValue / 100f;
                                DamageValue = (float)__instance.maxHp * num6;
                                foreach (BarrierBuf barrierBuf4 in barrierBufList.ToArray())
                                {
                                        DamageValue = barrierBuf4.UseBarrier(RwbpType.P, DamageValue);
                                }
                                __instance.hp -= DamageValue;
                                __instance.Equipment.kitCreature?.script.kitEvent.OnTakeDamagePhysical(__instance, DamageValue);
                                __instance.MakeDamageEffect(RwbpType.P, DamageValue, __instance.defense.GetDefenseType(RwbpType.P));
                        }
                        else if (dmg.type == RwbpType.N)
                        {
                                __instance.hp -= DamageValue;
                                __instance.Equipment.kitCreature?.script.kitEvent.OnTakeDamagePhysical(__instance, DamageValue);
                                __instance.MakeDamageEffect(RwbpType.N, DamageValue, DefenseInfo.Type.NONE);
                        }
                        if (DamageValue > 0f && __instance.defense.GetMultiplier(dmg.type) > 1f)
                        {
                                __instance.AddUnitBuf(new UnderAttackBuf());
                        }
                        if (__instance.IsPanic() && (__instance.CurrentPanicAction is PanicRoaming || __instance.CurrentPanicAction is PanicOpenIsolate) && actor is AgentModel model && !model.CannotControll() && !model.IsPanic())
                        {
                                __instance.AddUnitBuf(new PanicUnderAttackBuf());
                        }
                        __instance.hp = Mathf.Clamp(__instance.hp, 0f, (float)__instance.maxHp);
                        __instance.mental = Mathf.Clamp(__instance.mental, 0f, (float)__instance.maxMental);
                        if (__instance.invincible)
                        {
                                return false;
                        }
                        if (__instance.hp <= 0f)
                        {
                                if (UnityEngine.Random.value <= 0.25f && __instance.isRealWorker)
                                {

                                        Traverse.Create(__instance).Field("_revivalHp").SetValue(true);
                                }
                                else
                                {
                                        Traverse.Create(__instance).Field("_revivalHp").SetValue(false);
                                }
                                if (!Traverse.Create(__instance).Field("_revivaledHp").GetValue<bool>() && Traverse.Create(__instance).Field("_revivalHp").GetValue<bool>()
                                    && MissionManager.instance.ExistsFinishedBossMission(SefiraEnum.CHESED) && __instance.DeadType != DeadType.EXECUTION)
                                {
                                        __instance.hp = 1f;
                                        Traverse.Create(__instance).Field("_revivaledHp").SetValue(true);
                                        Traverse.Create(__instance).Field("_revivalHp").SetValue(false);
                                        __instance.RecoverHP((float)__instance.maxHp / 2f);
                                }
                                else
                                {
                                        if (dmg.specialDeadSceneEnable)
                                        {
                                                __instance.SetSpecialDeadScene(dmg.specialDeadSceneName);
                                        }
                                        __instance.OnDie();
                                }
                        }
                        else if (__instance.IsPanic())
                        {

                                if (__instance.mental >= __instance.maxMental)
                                {
                                        __instance.StopPanic();
                                }
                        }
                        else if (__instance.mental <= 0f)
                        {
                                if (UnityEngine.Random.value <= 0.25f && __instance.isRealWorker)
                                {
                                        Traverse.Create(__instance).Field("_revivalMental").SetValue(true);
                                }

                                else
                                {
                                        Traverse.Create(__instance).Field("_revivalMental").SetValue(false);
                                }
                                if (
                                    !Traverse.Create(__instance).Field("_revivaledMental").GetValue<bool>() &&
                                    Traverse.Create(__instance).Field("_revivalMental").GetValue<bool>()
                                    && MissionManager.instance.ExistsFinishedBossMission(SefiraEnum.CHESED))
                                {
                                        Traverse.Create(__instance).Field("_revivaledMental").SetValue(true);
                                        Traverse.Create(__instance).Field("_revivalMental").SetValue(false);
                                        __instance.RecoverMental((float)__instance.maxMental / 2f);


                                        //Active Chesed Ability


                                }
                                else
                                {
                                        __instance.Panic();
                                }
                        }
                        __instance.Equipment.armor?.OnTakeDamage_After(DamageValue, dmg.type);
                        __instance.Equipment.weapon?.OnTakeDamage_After(DamageValue, dmg.type);
                        __instance.Equipment.gifts.OnTakeDamage_After(DamageValue, dmg.type);
                        return false;
                }


                //兔子单位
                [HarmonyPrefix, HarmonyPatch(typeof(RabbitModel), "TakeDamage")]
                public static bool HP_RabbitModelTkDmg(UnitModel actor, DamageInfo dmg, RabbitModel __instance)
                {
                        dmg = dmg.Copy();
                        List<BarrierBuf> barrierBufList = (List<BarrierBuf>)Traverse.Create(__instance).Field("_barrierBufList").GetValue();
                        if (__instance.IsDead())
                        {
                                return false;
                        }
                        float num = 1f;
                        if (actor != null)
                        {
                                num = UnitModel.GetDmgMultiplierByEgoLevel(actor.GetAttackLevel(), __instance.GetDefenseLevel());
                        }
                        num *= __instance.GetBufDamageMultiplier(actor, dmg);
                        float num2 = dmg.GetDamageWithDefenseInfo(__instance.defense) * num;
                        if (dmg.type == RwbpType.R)
                        {
                                foreach (BarrierBuf barrierBuf in barrierBufList.ToArray())
                                {
                                        num2 = barrierBuf.UseBarrier(RwbpType.R, num2);
                                }
                                __instance.hp -= num2;
                                __instance.MakeDamageEffect(RwbpType.R, num2, __instance.defense.GetDefenseType(RwbpType.R));
                        }
                        else if (dmg.type == RwbpType.W)
                        {
                                foreach (BarrierBuf barrierBuf2 in barrierBufList.ToArray())
                                {
                                        num2 = barrierBuf2.UseBarrier(RwbpType.W, num2);
                                }
                                __instance.mental -= num2;
                                __instance.MakeDamageEffect(RwbpType.W, num2, __instance.defense.GetDefenseType(RwbpType.W));
                        }
                        else if (dmg.type == RwbpType.B)
                        {
                                foreach (BarrierBuf barrierBuf3 in barrierBufList.ToArray())
                                {
                                        num2 = barrierBuf3.UseBarrier(RwbpType.B, num2);
                                }
                                __instance.hp -= num2;
                                __instance.mental -= num2;
                                __instance.MakeDamageEffect(RwbpType.B, num2, __instance.defense.GetDefenseType(RwbpType.B));
                        }
                        else if (dmg.type == RwbpType.P)
                        {
                                float num3 = num2 / 100f;
                                num2 = (float)__instance.maxHp * num3;
                                foreach (BarrierBuf barrierBuf4 in barrierBufList.ToArray())
                                {
                                        num2 = barrierBuf4.UseBarrier(RwbpType.P, num2);
                                }
                                __instance.hp -= num2;
                                __instance.MakeDamageEffect(RwbpType.P, num2, __instance.defense.GetDefenseType(RwbpType.P));
                        }
                        else if (dmg.type == RwbpType.N)
                        {
                                __instance.hp -= num2;
                                __instance.MakeDamageEffect(RwbpType.N, num2, DefenseInfo.Type.NONE);
                        }
                        __instance.hp = Mathf.Clamp(__instance.hp, 0f, (float)__instance.maxHp);
                        __instance.mental = Mathf.Clamp(__instance.mental, 0f, (float)__instance.maxMental);
                        if (__instance.hp <= 0f)
                        {
                                __instance.OnDie();
                        }
                        else if (__instance.mental <= 0f)
                        {
                                __instance.OnDieByMental();
                        }
                        return false;
                }


                //真正麻烦的是许多MOD为了实装自己独特的伤害机制，这个东西经常被Patch

                //------------------------



                //敌对单位血量UI显示小数

                [HarmonyPostfix, HarmonyPatch(typeof(CreatureUnit), "Update")]
                public static void HP_CreatureUnit(CreatureUnit __instance)
                {
                        int acc = LCBMConfig.Precision_CreatureHP.Value;
                        if (__instance.hpSlider.gameObject.activeInHierarchy)
                        {
                                //移除之前的血量信息
                                int startIndex = __instance.escapeCreatureName.text.IndexOf(' ');
                                int endIndex = __instance.escapeCreatureName.text.IndexOf(']');
                                if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                                {
                                        __instance.escapeCreatureName.text = __instance.escapeCreatureName.text.Remove(startIndex, endIndex - startIndex + 1);
                                }
                                //更新血量
                                string curhp = string.Format("{0:F" + acc + "}", __instance.model.hp);
                                string _hp = $" [{curhp}/{__instance.model.maxHp}]";
                                __instance.escapeCreatureName.text += _hp;
                        }


                }


                [HarmonyPostfix, HarmonyPatch(typeof(ChildCreatureUnit), "Update")]
                public static void HP_ChildCreatureUnit(ChildCreatureUnit __instance)
                {
                        HP_CreatureUnit(__instance);
                }



                //员工hp,mp显示小数

                [HarmonyPostfix, HarmonyPatch(typeof(AgentSlot), "UpdateUI")]
                public static void HP_AgentSlot(AgentSlot __instance)
                {
                        int acc = LCBMConfig.Precision_AgentStat.Value;
                        string curhp = string.Format("{0:F" + acc + "}", __instance.CurrentAgent.hp);
                        string curmp = string.Format("{0:F" + acc + "}", __instance.CurrentAgent.mental);
                        __instance.HealthText.text = $"{curhp}/{__instance.CurrentAgent.maxHp}";
                        __instance.MentalText.text = $"{curmp}/{__instance.CurrentAgent.maxMental}";
                }

        }
}
