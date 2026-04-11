using System.Collections.Generic;
using RimWorld;
using Verse;

namespace BigLittleModPatch
{
    [StaticConstructorOnStartup]
    public static class Fix_DuplicateLinkableFacilities
    {
        static Fix_DuplicateLinkableFacilities()
        {
            int totalRemoved = 0;
            foreach (ThingDef def in DefDatabase<ThingDef>.AllDefs)
            {
                CompProperties_AffectedByFacilities comp = def.GetCompProperties<CompProperties_AffectedByFacilities>();
                if (comp?.linkableFacilities == null)
                    continue;

                HashSet<ThingDef> seen = new HashSet<ThingDef>();
                for (int i = comp.linkableFacilities.Count - 1; i >= 0; i--)
                {
                    if (!seen.Add(comp.linkableFacilities[i]))
                    {
                        comp.linkableFacilities.RemoveAt(i);
                        totalRemoved++;
                    }
                }
            }
            if (totalRemoved > 0)
                Log.Message("[BigLittleModPatch] Removed " + totalRemoved + " duplicate linkableFacilities entries.");
        }
    }
}
