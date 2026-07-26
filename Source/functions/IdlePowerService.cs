using System.Linq;
using RimWorld;
using Verse;

namespace RW_Repower.functions
{
    /// <summary>
    /// Applies RePower's idle state to eligible player-owned buildings.
    /// </summary>
    internal static class IdlePowerService
    {
        public static bool CanSetIdle(Building building)
        {
            return building != null
                && building.Spawned
                && building.Faction == Faction.OfPlayer
                && RePower.Things.Contains(building.def);
        }

        public static void SetIdle(Building building)
        {
            if (!CanSetIdle(building))
                return;

            RePower.SetPower(building, IsActive: false);
        }

        public static void SetAllLoadedBuildingsIdle()
        {
            var maps = Find.Maps;
            if (maps == null || RePower.Things == null || RePower.Things.Count == 0)
                return;

            foreach (var map in maps)
            {
                if (map == null)
                    continue;

                foreach (var buildingDef in RePower.Things)
                {
                    foreach (var building in map.listerBuildings.AllBuildingsColonistOfDef(buildingDef))
                    {
                        SetIdle(building);
                    }
                }
            }
        }
    }
}
