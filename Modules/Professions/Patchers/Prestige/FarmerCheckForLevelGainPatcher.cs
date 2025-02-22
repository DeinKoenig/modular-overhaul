﻿namespace DaLion.Overhaul.Modules.Professions.Patchers.Prestige;

#region using directives

using DaLion.Shared.Harmony;
using HarmonyLib;

#endregion using directives

[UsedImplicitly]
internal sealed class FarmerCheckForLevelGainPatcher : HarmonyPatcher
{
    /// <summary>Initializes a new instance of the <see cref="FarmerCheckForLevelGainPatcher"/> class.</summary>
    internal FarmerCheckForLevelGainPatcher()
    {
        this.Target = this.RequireMethod<Farmer>(nameof(Farmer.checkForLevelGain));
    }

    #region harmony patches

    /// <summary>Patch to allow level increase up to 20.</summary>
    [HarmonyPostfix]
    private static void FarmerCheckForLevelGainPostfix(ref int __result, int oldXP, int newXP)
    {
        if (!ProfessionsModule.Config.EnablePrestige || !ProfessionsModule.Config.EnableExtendedProgression)
        {
            return;
        }

        for (var i = 1; i <= 10; i++)
        {
            var requiredExpForThisLevel = ISkill.ExpAtLevel10 + (ProfessionsModule.Config.RequiredExpPerExtendedLevel * i);
            if (oldXP >= requiredExpForThisLevel)
            {
                continue;
            }

            if (newXP < requiredExpForThisLevel)
            {
                return;
            }

            __result = i + 10;
        }
    }

    #endregion harmony patches
}
