using System;
using CreatureInfo;
using HarmonyLib;
using UnityEngine;

namespace LCBM.Harmony
{
        //unuse
        internal class HP_CreatureInfoUI
        {

                //修复图鉴的计数器显示BUG
                [HarmonyPostfix]
                [HarmonyPatch(typeof(CreatureInfoEscapeRoot))]
                [HarmonyPatch("Initialize", MethodType.Normal)]
                [HarmonyPatch(new Type[] { })]
                public static void HP_Initialize(CreatureInfoEscapeRoot __instance)
                {

                        string CounterText = "X";


                        if (__instance.CurrentModel != null && __instance.CurrentModel.script.GetQliphothCounterMax() > 0)
                        {
                                CounterText = __instance.CurrentModel.script.GetQliphothCounterMax().ToString();
                        }
                        else if (__instance.MetaInfo != null && __instance.MetaInfo.qliphothMax > 0)
                        {
                                CounterText = __instance.MetaInfo.qliphothMax.ToString();
                        }


                        __instance.QliphothCounterText.text = CounterText;

                }


                [HarmonyPostfix]
                [HarmonyPatch(typeof(CreatureInfoEquipmentRoot))]
                [HarmonyPatch("Initialize", MethodType.Normal)]
                [HarmonyPatch(new Type[] { })]
                public static void HP_Initialize(CreatureInfoEquipmentRoot __instance)
                {


                        WeaponSlot weaponSlot = __instance.weaponSlot;
                        weaponSlot.DamageRange.text = weaponSlot.DamageRange.text.Replace("-", "~");
                        weaponSlot.AttackSpeed.horizontalOverflow = HorizontalWrapMode.Overflow;
                        weaponSlot.AttackRange.horizontalOverflow = HorizontalWrapMode.Overflow;
                }




        }
}
