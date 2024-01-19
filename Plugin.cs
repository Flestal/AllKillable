using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace AllKillable
{
    [BepInPlugin("Flestal.AllKillable", "All Enemies are Killable", "1.0.0")]
    public class Plugin:BaseUnityPlugin
    {
        public static Plugin Instance;
        public static Harmony harmony;
        private void Awake()
        {
            Plugin.harmony = new Harmony("Flestal.AllKillable");
            bool flag = Plugin.Instance == null;
            if (flag)
            {
                Plugin.Instance = this;
            }
            Plugin.harmony.PatchAll(typeof(Plugin));
            Plugin.CreateHarmonyPatch(Plugin.harmony, typeof(EnemyAI), "HitEnemy", new Type[]{
                typeof(int),
                typeof(PlayerControllerB),
                typeof(bool)
            }, typeof(Patches_AllKillAble), "AllKillAblePatch", false);
        }

        public static void CreateHarmonyPatch(Harmony harmony, Type typeToPatch, string methodToPatch, Type[] parameters, Type patchType, string patchMethod, bool isPrefix)
        {
            bool flag = typeToPatch == null || patchType == null;
            if (flag)
            {
                Debug.Log("Type is either incorrect or does not exist!");
            }
            else
            {
                MethodInfo Method = AccessTools.Method(typeToPatch, methodToPatch, parameters, null);
                MethodInfo Patch_Method = AccessTools.Method(patchType, patchMethod, null, null);
                if (isPrefix)
                {
                    harmony.Patch(Method, new HarmonyMethod(Patch_Method), null, null, null, null);
                    Debug.Log("Prefix " + Method.Name + " Patched!");
                }
                else
                {
                    harmony.Patch(Method, null, new HarmonyMethod(Patch_Method), null, null, null);
                    Debug.Log("Postfix " + Method.Name + " Patched!");
                }
            }
        }
    }
    
}
