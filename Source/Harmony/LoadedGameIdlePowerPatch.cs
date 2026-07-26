using RW_Repower.functions;
using Verse;

namespace RW_Repower.Harmony
{
    /// <summary>
    /// Restores RePower's idle state after RimWorld has finished loading a save.
    /// </summary>
    [HarmonyLib.HarmonyPatch(typeof(Game), nameof(Game.LoadGame))]
    internal static class LoadedGameIdlePowerPatch
    {
        private static void Postfix()
        {
            LongEventHandler.ExecuteWhenFinished(IdlePowerService.SetAllLoadedBuildingsIdle);
        }
    }
}
