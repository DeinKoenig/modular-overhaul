﻿namespace DaLion.Overhaul.Modules.Arsenal.Patchers.Forges;

#region using directives

using DaLion.Shared.Harmony;
using HarmonyLib;
using StardewValley.Tools;

#endregion using directives

[UsedImplicitly]
internal sealed class AmethystEnchantmentApplyToPatcher : HarmonyPatcher
{
    /// <summary>Initializes a new instance of the <see cref="AmethystEnchantmentApplyToPatcher"/> class.</summary>
    internal AmethystEnchantmentApplyToPatcher()
    {
        this.Target = this.RequireMethod<AmethystEnchantment>("_ApplyTo");
    }

    #region harmony patches

    /// <summary>Rebalances Amethyst enchant.</summary>
    [HarmonyPrefix]
    private static bool AmethystEnchantmentApplyToPrefix(AmethystEnchantment __instance, Item item)
    {
        if (item is not MeleeWeapon weapon || !ArsenalModule.Config.RebalancedForges)
        {
            return true; // run original logic
        }

        weapon.knockback.Value += __instance.GetLevel() * 0.1f;
        return false; // don't run original logic
    }

    #endregion harmony patches
}
