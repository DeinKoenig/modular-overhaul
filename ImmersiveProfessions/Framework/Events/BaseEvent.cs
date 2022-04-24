﻿namespace DaLion.Stardew.Professions.Framework.Events;

#region using directives

using StardewModdingAPI.Utilities;

#endregion using directives

/// <summary>Base implementation for an event wrapper allowing dynamic enabling / disabling.</summary>
internal abstract class BaseEvent : IEvent
{
    protected readonly PerScreen<bool> enabled = new();

    public bool IsEnabled => enabled.Value;

    /// <inheritdoc />
    public void Enable()
    {
        enabled.Value = true;
    }

    /// <inheritdoc />
    public void Disable()
    {
        enabled.Value = false;
    }

    /// <summary>Whether this event is enabled for a specific splitscreen player.</summary>
    /// <param name="screenId">The player's screen id.</param>
    public bool IsEnabledForScreen(int screenId)
    {
        return enabled.GetValueForScreen(screenId);
    }
}