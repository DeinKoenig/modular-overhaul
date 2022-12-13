﻿namespace DaLion.Overhaul.Modules.Arsenal.Patchers;

#region using directives

using DaLion.Shared.Extensions;
using DaLion.Shared.Extensions.Collections;
using DaLion.Shared.Extensions.Stardew;
using DaLion.Shared.Harmony;
using DaLion.Shared.Networking;
using HarmonyLib;

#endregion using directives

[UsedImplicitly]
internal sealed class FarmerFoundArtifactPatcher : HarmonyPatcher
{
    /// <summary>Initializes a new instance of the <see cref="FarmerFoundArtifactPatcher"/> class.</summary>
    internal FarmerFoundArtifactPatcher()
    {
        this.Target = this.RequireMethod<Farmer>(nameof(Farmer.foundArtifact));
    }

    #region harmony patches

    /// <summary>Trigger blueprint reward.</summary>
    [HarmonyPrefix]
    private static bool FarmerFoundArtifactPrefix(Farmer __instance, int index)
    {
        if (Globals.DwarvishBlueprintIndex.HasValue || index != Globals.DwarvishBlueprintIndex!.Value)
        {
            return true; // run original logic
        }

        if (!Globals.ElderwoodIndex.HasValue)
        {
            return false; // don't run original logic
        }

        var player = Game1.player;
        var found = player.Read(DataFields.BlueprintsFound).ParseList<int>();
        int blueprint;
        if (!found.ContainsAny(Constants.ElfBladeIndex, Constants.ForestSwordIndex))
        {
            blueprint = Game1.random.NextDouble() < 0.5 ? Constants.ElfBladeIndex : Constants.ForestSwordIndex;
        }
        else
        {
            blueprint = found.Contains(Constants.ElfBladeIndex) ? Constants.ForestSwordIndex : Constants.ElfBladeIndex;
        }

        player.Append(DataFields.BlueprintsFound, blueprint.ToString());
        if (player.Read(DataFields.BlueprintsFound).ParseList<int>().Count == 6)
        {
            player.completeQuest(Constants.ForgeNextQuestId);
        }

        if (!player.hasOrWillReceiveMail("dwarvishBlueprintFound"))
        {
            Game1.player.mailReceived.Add("dwarvishBlueprintFound");
        }

        player.holdUpItemThenMessage(new SObject(Globals.DwarvishBlueprintIndex.Value, 1));
        if (Context.IsMultiplayer)
        {
            Broadcaster.SendPublicChat(I18n.Get("blueprint.found.global", new { who = player.Name }));
        }

        return false; // don't run original logic
    }

    #endregion harmony patches
}
