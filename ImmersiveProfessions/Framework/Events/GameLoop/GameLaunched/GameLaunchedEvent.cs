﻿namespace DaLion.Stardew.Professions.Framework.Events.GameLoop;

#region using directives

using StardewModdingAPI.Events;

#endregion using directives

/// <summary>Wrapper for <see cref="IGameLoopEvents.GameLaunched"/> allowing dynamic enabling / disabling.</summary>
internal abstract class GameLaunchedEvent : BaseEvent
{
    /// <summary>Raised after the game is launched, right before the first update tick.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event data.</param>
    public void OnGameLaunched(object sender, GameLaunchedEventArgs e)
    {
        if (enabled.Value || GetType().Name.StartsWith("Static")) OnGameLaunchedImpl(sender, e);
    }

    /// <inheritdoc cref="OnGameLaunched" />
    protected abstract void OnGameLaunchedImpl(object sender, GameLaunchedEventArgs e);
}