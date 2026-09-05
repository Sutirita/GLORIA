using Assets.Scripts.UI.Utils;
using Credit;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LCBM.Harmony
{
        //unuse

        class HP_Basic
        {
                //标题界面1
                [HarmonyPostfix, HarmonyPatch(typeof(NewTitleScript), "Start")]
                public static void HP_NTCS(NewTitleScript __instance)
                {
                        //显示版本号
                        __instance.GameVersionChecker.text += LCBMStaticData.PluginVertionDescStr;

                        //添加MOD按钮
                        //MOD管理页面待做，暂时复制设置按钮

                        GameObject ButtonArea = GameObject.Find("TitleCanvas").transform.GetChild(2).GetChild(4).GetChild(0).gameObject;
                        GameObject NewOption = GameObject.Instantiate(ButtonArea.transform.GetChild(6).gameObject, ButtonArea.transform);
                        GameObject Frame = NewOption.transform.GetChild(1).gameObject;
                        NewOption.name = "MOD_Option";
                        NewOption.transform.SetSiblingIndex(6);
                        NewOption.transform.GetChild(0).GetComponent<LocalizeTextLoadScript>().SetTextForcely("MOD");
                        NewOption.transform.localPosition = new Vector3(355, 95.5f, 0);

                        if (NewOption is null || Frame is null) return;


                        EventTrigger et = NewOption.GetComponent<EventTrigger>();
                        if (et is null) et = NewOption.AddComponent<EventTrigger>();
                        et.triggers.RemoveAll(e => e.eventID == EventTriggerType.PointerEnter || e.eventID == EventTriggerType.PointerExit);

                        // 鼠标移入
                        var enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
                        enter.callback.AddListener(_ => Frame.SetActive(true));
                        et.triggers.Add(enter);

                        // 鼠标移出
                        var exit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
                        exit.callback.AddListener(_ => Frame.SetActive(false));
                        et.triggers.Add(exit);

                        // 默认隐藏
                        Frame.SetActive(false);







                }




                //标题界面2

                [HarmonyPostfix, HarmonyPatch(typeof(AlterTitleController), "Start")]
                public static void HP_ATCS(AlterTitleController __instance)
                {
                        //显示版本号
                        __instance.GameVersionChecker.text += LCBMStaticData.PluginVertionDescStr;


                        //添加MOD按钮
                        //MOD管理页面待做，暂时复制设置按钮
                        GameObject ButtonLayout = GameObject.Find("AlterTitleRoot").transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
                        RectTransform rect = ButtonLayout.GetComponent<RectTransform>();
                        rect.anchoredPosition += new Vector2(0, 80);
                        GameObject Settting_Option = ButtonLayout.transform.GetChild(4).gameObject;
                        GameObject NewOption = GameObject.Instantiate(Settting_Option, ButtonLayout.transform);
                        NewOption.name = "MOD_Option";
                        NewOption.transform.SetSiblingIndex(5);
                        NewOption.transform.GetChild(1).GetComponent<LocalizeTextLoadScript>().SetTextForcely("MOD");

                }



        }

}
