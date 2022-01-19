﻿namespace DaLion.Stardew.Professions.Framework.Patches.Prestige;

#region using directives

using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using StardewValley;

#endregion using directive

[UsedImplicitly]
internal class GameLocationCanRespecPatch : BasePatch
{
    /// <summary>Construct an instance.</summary>
    internal GameLocationCanRespecPatch()
    {
        Original = RequireMethod<GameLocation>(nameof(GameLocation.canRespec));
    }

    #region harmony patches

    /// <summary>Patch to change Statue of Uncertainty respec from <10 to>10.</summary>
    [HarmonyPrefix]
    private static bool GameLocationCanRespecPrefix(ref bool __result, int skill_index)
    {
        if (!ModEntry.Config.EnablePrestige) return true; // run original logic

        try
        {
            __result = Game1.player.GetUnmodifiedSkillLevel(skill_index) >= 15 &&
                       !Game1.player.newLevels.Contains(new(skill_index, 15)) &&
                       !Game1.player.newLevels.Contains(new(skill_index, 20));
            return false; // don't run original logic;
        }
        catch (Exception ex)
        {
            Log.E($"Failed in {MethodBase.GetCurrentMethod()?.Name}:\n{ex}");
            return true; // default to original logic
        }
    }

    #endregion harmony patches
}