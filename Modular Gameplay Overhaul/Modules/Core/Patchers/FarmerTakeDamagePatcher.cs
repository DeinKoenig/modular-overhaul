﻿namespace DaLion.Overhaul.Modules.Core.Patchers;

#region using directives

using DaLion.Overhaul.Modules.Core.VirtualProperties;
using DaLion.Shared.Harmony;
using HarmonyLib;

#endregion using directives

[UsedImplicitly]
internal sealed class FarmerTakeDamagePatcher : HarmonyPatcher
{
    /// <summary>Initializes a new instance of the <see cref="FarmerTakeDamagePatcher"/> class.</summary>
    internal FarmerTakeDamagePatcher()
    {
        this.Target = this.RequireMethod<Farmer>(nameof(Farmer.takeDamage));
    }

    #region harmony patches

    /// <summary>Reset seconds out of combat.</summary>
    [HarmonyPostfix]
    private static void FarmerTakeDamagePostfix(Farmer __instance)
    {
        __instance.Set_SecondsOutOfCombat(0);
    }

    #endregion harmony patches
}
