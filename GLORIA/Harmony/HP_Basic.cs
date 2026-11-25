using Assets.Scripts.UI.Utils;
using Credit;
using GLORIA.API;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GLORIA.Harmony
{
        //unuse

        class HP_Basic
        {
                //标题界面1
                [HarmonyPostfix]
                [HarmonyPatch(typeof(NewTitleScript), "Start")]
                public static void HP_NTCS(NewTitleScript __instance)
                {
                        //显示版本号
                        __instance.GameVersionChecker.text += StaticData.PluginVertionDescStr;

                        //添加MOD按钮
                        //MOD管理页面待做，暂时复制设置按钮

                        GameObject ButtonArea = GameObject.Find("TitleCanvas/InnerLayer/MenuFrame/ButtonArea/");

                        GameObject NewOption = GameObject.Instantiate(ButtonArea.transform.GetChild(4).gameObject, ButtonArea.transform);

                        NewOption.name = "MOD_Option";

                        NewOption.transform.SetSiblingIndex(6);

                        GameObject Frame = NewOption.transform.GetChild(1).gameObject;

                        GameObject ModNum =Frame.transform.GetChild(0).gameObject;

                        ModNum.name = "Mod_Num";
                        GameObject ModNum_Desc = ModNum.transform.GetChild(0).gameObject;

                        Text ModNum_Desc_TextComp = ModNum_Desc.GetComponent<Text>();

                        ModNum_Desc_TextComp.text = $"已加载 {GLOBAL.ModlRegistry.Mods.Count()} 个模组";

                        ModNum_Desc_TextComp.fontSize = 8;
                                
                        GameObject Text = NewOption.transform.GetChild(0).gameObject;

                        Text.GetComponent<LocalizeTextLoadScript>().SetTextForcely("MOD");





                        NewOption.transform.localPosition = new Vector3(409.4955f,80.6365f, 0);



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

                [HarmonyPostfix]
                [HarmonyPatch(typeof(AlterTitleController), "Start")]
                public static void HP_ATCS(AlterTitleController __instance)
                {
                        //显示版本号
                        __instance.GameVersionChecker.text += StaticData.PluginVertionDescStr;


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








                [HarmonyPatch(typeof(GameStaticDataLoader), "LoadResearchDescData")]
                public static class LoadResearchDescData_LogPatch
                {
                        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
                        {
                                var codes = new List<CodeInstruction>(instructions);

                                var logMethod = AccessTools.Method(
                                    typeof(Debug),
                                    nameof(Debug.Log),
                                    new[] { typeof(object) }
                                );
                                for (int i = 0; i < codes.Count; i++)
                                {
                                        if (!codes[i].Calls(logMethod))
                                                continue;

                                        if (i > 0 && codes[i - 1].opcode == OpCodes.Ldloc_S)
                                        {
                                                codes.RemoveRange(i - 1, 2);
                                        }

                                        break;
                                }

                                return codes;
                        }
                }














        }

}
