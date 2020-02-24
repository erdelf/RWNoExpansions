using System.Linq;

namespace NoExpansions
{
    using System.Reflection;
    using JetBrains.Annotations;
    using RimWorld;
    using Verse;

    [StaticConstructorOnStartup]
    [UsedImplicitly]
    public static class NoExpansions
    {
        static NoExpansions()
        {
            MethodInfo info = typeof(DefDatabase<ExpansionDef>).GetMethod("Remove", BindingFlags.NonPublic | BindingFlags.Static);
            ExpansionDef[] expansions =
                DefDatabase<ExpansionDef>.AllDefs.Where(predicate: ed => ed.Status == ExpansionStatus.NotInstalled).ToArray();
            foreach (ExpansionDef def in expansions)
                info.Invoke(null, new object[] { def });
            typeof(ModLister).GetField("AllExpansionsCached", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, null);
        }
    }
}
