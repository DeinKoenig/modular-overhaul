﻿namespace DaLion.Overhaul.Modules.Combat.Patchers.Melee;

#region using directives

using DaLion.Shared.Harmony;
using HarmonyLib;
using StardewValley.Tools;

#endregion using directives

[UsedImplicitly]
internal sealed class MeleeWeaponLeftClickPatcher : HarmonyPatcher
{
    /// <summary>Initializes a new instance of the <see cref="MeleeWeaponLeftClickPatcher"/> class.</summary>
    internal MeleeWeaponLeftClickPatcher()
    {
        this.Target = this.RequireMethod<MeleeWeapon>(nameof(MeleeWeapon.leftClick));
    }

    #region harmony patches

    /// <summary>Eliminate dumb vanilla weapon spam.</summary>
    [HarmonyPrefix]
    private static bool MeleeWeaponLeftClickPrefix(MeleeWeapon __instance)
    {
        return !CombatModule.Config.EnableWeaponOverhaul || !CombatModule.Config.EnableMeleeComboHits ||
               __instance.type.Value == MeleeWeapon.dagger;
    }

    #endregion harmony patches
}
