﻿namespace DaLion.Overhaul.Modules.Weapons.Integrations;

#region using directives

using DaLion.Overhaul.Modules.Weapons.Events;
using DaLion.Shared.Attributes;
using DaLion.Shared.Integrations;

#endregion using directives

[RequiresMod("FlashShifter.StardewValleyExpandedCP", "StardewValleyExpanded")]
internal sealed class StardewValleyExpandedIntegration : ModIntegration<StardewValleyExpandedIntegration>
{
    private StardewValleyExpandedIntegration()
        : base(
            "FlashShifter.StardewValleyExpandedCP",
            "StardewValleyExpanded",
            null,
            ModHelper.ModRegistry)
    {
    }

    protected override bool RegisterImpl()
    {
        if (!this.IsLoaded)
        {
            return false;
        }

        EventManager.Enable<SveWarpedEvent>();
        return true;
    }
}
