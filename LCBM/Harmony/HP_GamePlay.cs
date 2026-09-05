using Customizing;
using GeburahBoss;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace LCBM.Harmony
{
        //unuse
        class HP_GamePlay
        {
                //修复ChesedBUG，同时重写LoadResearchDescData，原版会在这吧所有语言条目在控制台输出一遍，不好看
                [HarmonyPrefix, HarmonyPatch(typeof(GameStaticDataLoader), "LoadResearchDescData")]
                public static bool HP_LoadResearchDescData(List<ResearchItemTypeInfo> research)
                {
                        foreach (ResearchItemTypeInfo researchItemTypeInfo in research)
                        {
                                if (researchItemTypeInfo.id == 803)
                                {
                                        researchItemTypeInfo.upgradeInfos[0].specialAbility = new ResearchSpecialAbility
                                        {
                                                name = "upgrade_recover_bullet"
                                        };
                                        researchItemTypeInfo.upgradeInfos[0].bulletAility = null;
                                }
                        }


                        XmlDocument xmlDocument = AssetLoader.LoadExternalXML("Language/ResearchDesc");
                        Dictionary<int, Dictionary<string, ResearchItemDesc>> dictionary = new Dictionary<int, Dictionary<string, ResearchItemDesc>>();
                        List<string> list = new List<string>();
                        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("root/supportLanguage/ln");
                        IEnumerator enumerator = xmlNodeList.GetEnumerator();
                        try
                        {
                                while (enumerator.MoveNext())
                                {
                                        object obj = enumerator.Current;
                                        XmlNode xmlNode = (XmlNode)obj;
                                        list.Add(xmlNode.InnerText);
                                }
                        }
                        finally
                        {
                                IDisposable disposable;
                                if ((disposable = (enumerator as IDisposable)) != null)
                                {
                                        disposable.Dispose();
                                }
                        }
                        XmlNodeList xmlNodeList2 = xmlDocument.SelectNodes("root/node");
                        IEnumerator enumerator2 = xmlNodeList2.GetEnumerator();
                        try
                        {
                                while (enumerator2.MoveNext())
                                {
                                        object obj2 = enumerator2.Current;
                                        XmlNode xmlNode2 = (XmlNode)obj2;
                                        int key = (int)float.Parse(xmlNode2.Attributes.GetNamedItem("id").InnerText);
                                        Dictionary<string, ResearchItemDesc> dictionary2 = new Dictionary<string, ResearchItemDesc>();
                                        foreach (string text in list)
                                        {
                                                XmlNode xmlNode3 = xmlNode2.SelectSingleNode(text);
                                                string innerText = xmlNode3.SelectSingleNode("name").InnerText;
                                                // Debug.Log(innerText);
                                                //烦人的要死
                                                string innerText2 = xmlNode3.SelectSingleNode("current").InnerText;
                                                string innerText3 = xmlNode3.SelectSingleNode("short").InnerText;
                                                innerText.Trim();
                                                innerText2.Trim();
                                                dictionary2.Add(text, new ResearchItemDesc
                                                {
                                                        name = innerText,
                                                        desc = innerText2,
                                                        shortDesc = innerText3
                                                });
                                        }
                                        dictionary.Add(key, dictionary2);
                                }
                        }
                        finally
                        {
                                IDisposable disposable2;
                                if ((disposable2 = (enumerator2 as IDisposable)) != null)
                                {
                                        disposable2.Dispose();
                                }
                        }
                        foreach (ResearchItemTypeInfo researchItemTypeInfo in research)
                        {
                                if (dictionary.TryGetValue(researchItemTypeInfo.id, out Dictionary<string, ResearchItemDesc> desc))
                                {
                                        researchItemTypeInfo.desc = desc;
                                }
                        }

                        return false;


                }






                //修复Geburah三阶段索敌BUG

                [HarmonyPrefix, HarmonyPatch(typeof(ThirdPhase), "GetNextAction")]
                public static bool HP_GeburahThirdPhaseFix(GeburahBoss.ThirdPhase __instance, ref GeburahAction __result, List<UnitModel> near)
                {
                        float moveProb = 0.2f;
                        float spearProb = 0.3f;

                        GeburahAction result;

                        if (near.Count == 0 || __instance.geburah.currentPassage is null)
                        {
                                if (__instance.geburah.currentPassage is null)
                                {
                                        result = new MoveNodeAction(__instance.geburah, __instance.GetRandomNode());

                                }
                                else
                                {
                                        if (UnityEngine.Random.value <= moveProb)

                                        {
                                                result = new MoveNodeAction(__instance.geburah, __instance.GetRandomNode());
                                        }
                                        else
                                        {
                                                result = new GeburahIdle(__instance.geburah, UnityEngine.Random.Range(3f, 5f));
                                        }
                                }
                        }
                        else
                        {
                                if (__instance.isPrevAttack)
                                {
                                        __instance.isPrevAttack = false;

                                        result = new GeburahIdle(__instance.geburah, false, GeburahStaticInfo.AttackDelay.GetRandomFloat());
                                }
                                else
                                {
                                        __instance.isPrevAttack = true;

                                        bool BloodyTreeFlag = __instance.geburah.CanStartBloodyTree();

                                        float DangoAttackProb = spearProb + (BloodyTreeFlag ? spearProb : 0f);

                                        if (BloodyTreeFlag && UnityEngine.Random.value <= spearProb)
                                        {
                                                result = new BloodyTreeThrow(__instance.geburah, false);
                                        }
                                        else
                                        {
                                                UnitModel TargetUnit = near[UnityEngine.Random.Range(0, near.Count)];

                                                if (__instance.geburah.IsInRange(TargetUnit, 10f))
                                                {
                                                        __instance.geburah.LookTarget(TargetUnit);
                                                        if (__instance.geburah.IsInRange(TargetUnit, 6f) && UnityEngine.Random.value <= DangoAttackProb)
                                                        {
                                                                result = new DangoAttackAction(__instance.geburah, false);
                                                        }
                                                        else
                                                        {
                                                                result = new DefaultAttack(__instance.geburah, GeburahStaticInfo.P3_LongBirdAttack.front, GeburahStaticInfo.P3_LongBirdAttack.rear, 1);
                                                        }
                                                }
                                                else
                                                {
                                                        result = new ChaseAction(__instance.geburah, TargetUnit.GetMovableNode(), 8f, true, false);
                                                }
                                        }
                                }
                        }

                        __result = result;

                        return false;
                }





                //修复员工属性升级bug

                [HarmonyPrefix]
                [HarmonyPatch(typeof(CustomizingWindow), "SetAgentStatBonus")]
                public static bool HP_SetAgentStatBonus(CustomizingWindow __instance, AgentModel agent, AgentData data)
                {
                        int level = agent.level;
                        agent.primaryStat.hp = __instance.SetRandomStatValue(agent.primaryStat.hp, agent.originFortitudeLevel, data.statBonus.rBonus);
                        agent.primaryStat.mental = __instance.SetRandomStatValue(agent.primaryStat.mental, agent.originPrudenceLevel, data.statBonus.wBonus);
                        agent.primaryStat.work = __instance.SetRandomStatValue(agent.primaryStat.work, agent.originTemperanceLevel, data.statBonus.bBonus);
                        agent.primaryStat.battle = __instance.SetRandomStatValue(agent.primaryStat.battle, agent.originJusticeLevel, data.statBonus.pBonus);
                        agent.UpdateTitle(level);
                        return false;
                }







        }
}
