﻿namespace DaLion.Overhaul.Modules.Weapons.Events;

#region using directives

using DaLion.Overhaul.Modules.Weapons.Integrations;
using DaLion.Shared.Events;
using StardewModdingAPI.Events;

#endregion using directives

[UsedImplicitly]
internal sealed class WeaponGameLaunchedEvent : GameLaunchedEvent
{
    /// <summary>Initializes a new instance of the <see cref="WeaponGameLaunchedEvent"/> class.</summary>
    /// <param name="manager">The <see cref="EventManager"/> instance that manages this event.</param>
    internal WeaponGameLaunchedEvent(EventManager manager)
        : base(manager)
    {
    }

    /// <inheritdoc />
    protected override void OnGameLaunchedImpl(object? sender, GameLaunchedEventArgs e)
    {
        // hard dependencies
        SpaceCoreIntegration.Instance!.Register();

        // soft dependencies or integrations
        JsonAssetsIntegration.Instance?.Register();
        StardewValleyExpandedIntegration.Instance?.Register();
        VanillaTweaksIntegration.Instance?.Register();
    }
}
