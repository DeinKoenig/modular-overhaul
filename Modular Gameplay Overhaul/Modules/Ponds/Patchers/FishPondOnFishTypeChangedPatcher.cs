﻿namespace DaLion.Overhaul.Modules.Ponds.Patchers;

#region using directives

using DaLion.Shared.Extensions.Stardew;
using DaLion.Shared.Harmony;
using HarmonyLib;
using StardewValley.Buildings;

#endregion using directives

[UsedImplicitly]
internal sealed class FishPondOnFishTypeChangedPatcher : HarmonyPatcher
{
    /// <summary>Initializes a new instance of the <see cref="FishPondOnFishTypeChangedPatcher"/> class.</summary>
    internal FishPondOnFishTypeChangedPatcher()
    {
        this.Target = this.RequireMethod<FishPond>(nameof(FishPond.OnFishTypeChanged));
    }

    #region harmony patches

    /// <summary>Record pre-reset state.</summary>
    [HarmonyPrefix]
    // ReSharper disable once RedundantAssignment
    private static void FishPondOnFishTypeChangedPrefix(FishPond __instance, ref int __state)
    {
        __state = __instance.fishType.Value;
    }

    /// <summary>Reset Fish Pond data.</summary>
    [HarmonyPostfix]
    private static void FishPondOnFishTypeChangedPostfix(FishPond __instance, int __state)
    {
        if (__state <= 0 || __instance.fishType.Value > 0)
        {
            return;
        }

        __instance.Write(DataFields.FishQualities, null);
        __instance.Write(DataFields.FamilyQualities, null);
        __instance.Write(DataFields.FamilyLivingHere, null);
        __instance.Write(DataFields.DaysEmpty, 0.ToString());
        __instance.Write(DataFields.SeaweedLivingHere, null);
        __instance.Write(DataFields.GreenAlgaeLivingHere, null);
        __instance.Write(DataFields.WhiteAlgaeLivingHere, null);
        __instance.Write(DataFields.CheckedToday, null);
        __instance.Write(DataFields.ItemsHeld, null);
    }

    #endregion harmony patches
}
