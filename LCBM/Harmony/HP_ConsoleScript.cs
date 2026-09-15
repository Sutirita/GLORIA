using HarmonyLib;
using System;
using UnityEngine;
namespace GLORIA.Harmony
{
        //unuse
        class HP_ConsoleScript
        {
                //劫持原版的控制台Update逻辑，使其失效。
                //TODO：制作一个更完善的控制台
                [HarmonyPrefix, HarmonyPatch(typeof(ConsoleScript), "Update")]
                public static bool DisableVanillaConsole(ConsoleScript __instance)
                {
                        return false;
                }

        }
}
