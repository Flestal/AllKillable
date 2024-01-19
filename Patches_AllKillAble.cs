using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllKillable
{
    internal class Patches_AllKillAble
    {
        public static void AllKillAblePatch(int force,PlayerControllerB playerWhoHit, bool playHitSFX,EnemyAI __instance)
        {
            if (force < 3) return;
            if (!__instance.enemyType.canDie)
            {
                __instance.enemyHP -= force;
                if(__instance.enemyHP <= 0)
                {
                    __instance.enemyType.canDie = true;
                    __instance.KillEnemyOnOwnerClient(false);
                }
            }else if (__instance.GetType() == typeof(ForestGiantAI))
            {
                __instance.enemyHP -= force;
                if (__instance.enemyHP <= 0)
                {
                    __instance.enemyType.canDie = true;
                    __instance.KillEnemyOnOwnerClient(false);
                }
            }
        }
    }
}
