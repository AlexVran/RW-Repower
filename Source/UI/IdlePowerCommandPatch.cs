using System.Collections.Generic;
using RW_Repower.functions;
using UnityEngine;
using Verse;

namespace RW_Repower.UI
{
    [StaticConstructorOnStartup]
    internal static class IdlePowerCommandTextures
    {
        public static readonly Texture2D SetIdle = ContentFinder<Texture2D>.Get(
            "UI/Commands/SetIdle",
            reportFailure: true);
    }

    /// <summary>
    /// Adds the manual idle command only to buildings managed by RePower.
    /// </summary>
    [HarmonyLib.HarmonyPatch(typeof(ThingWithComps), nameof(ThingWithComps.GetGizmos))]
    internal static class IdlePowerCommandPatch
    {
        private static void Postfix(ThingWithComps __instance, ref IEnumerable<Gizmo> __result)
        {
            __result = AppendIdleCommand(__result, __instance as Building);
        }

        private static IEnumerable<Gizmo> AppendIdleCommand(
            IEnumerable<Gizmo> existingGizmos,
            Building building)
        {
            if (existingGizmos != null)
            {
                foreach (var gizmo in existingGizmos)
                    yield return gizmo;
            }

            if (!IdlePowerService.CanSetIdle(building))
                yield break;

            yield return new Command_Action
            {
                defaultLabel = "RePower_SetIdle_Label".Translate(),
                defaultDesc = "RePower_SetIdle_Description".Translate(),
                icon = IdlePowerCommandTextures.SetIdle,
                action = () => IdlePowerService.SetIdle(building)
            };
        }
    }
}
