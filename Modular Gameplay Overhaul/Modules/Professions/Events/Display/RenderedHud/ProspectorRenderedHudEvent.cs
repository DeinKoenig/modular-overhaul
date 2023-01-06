﻿namespace DaLion.Overhaul.Modules.Professions.Events.Display;

#region using directives

using System.Linq;
using DaLion.Overhaul.Modules.Professions.Extensions;
using DaLion.Shared.Events;
using Microsoft.Xna.Framework;
using StardewModdingAPI.Events;
using StardewValley.Locations;

#endregion using directives

[UsedImplicitly]
internal sealed class ProspectorRenderedHudEvent : RenderedHudEvent
{
    /// <summary>Initializes a new instance of the <see cref="ProspectorRenderedHudEvent"/> class.</summary>
    /// <param name="manager">The <see cref="EventManager"/> instance that manages this event.</param>
    internal ProspectorRenderedHudEvent(EventManager manager)
        : base(manager)
    {
    }

    /// <inheritdoc />
    protected override void OnRenderedHudImpl(object? sender, RenderedHudEventArgs e)
    {
        if (ProfessionsModule.Config.DisableAlwaysTrack && !ProfessionsModule.Config.ModKey.IsDown())
        {
            return;
        }

        var shouldHighlightOnScreen = ProfessionsModule.Config.ModKey.IsDown();

        // track objects, such as ore nodes
        foreach (var (tile, _) in Game1.currentLocation.Objects.Pairs.Where(p =>
                     p.Value.ShouldBeTrackedBy(Profession.Prospector)))
        {
            tile.TrackWhenOffScreen(Color.Yellow);
            if (shouldHighlightOnScreen)
            {
                tile.TrackWhenOnScreen(Color.Yellow);
            }
        }

        // track resource clumps
        foreach (var clump in Game1.currentLocation.resourceClumps.Where(c =>
                     Collections.ResourceClumpIds.Contains(c.parentSheetIndex.Value)))
        {
            var tile = clump.tile.Value + new Vector2(0.5f, 0f);
            tile.TrackWhenOffScreen(Color.Yellow);
            if (shouldHighlightOnScreen)
            {
                tile.TrackWhenOnScreen(Color.Yellow);
            }
        }

        // track panning spots
        if (!Game1.currentLocation.orePanPoint.Value.Equals(Point.Zero))
        {
            var tile = Game1.currentLocation.orePanPoint.Value.ToVector2() * 64f;
            tile.TrackWhenOffScreen(Color.Lime);
            if (shouldHighlightOnScreen)
            {
                tile.TrackWhenOnScreen(Color.Lime);
            }
        }

        if (Game1.currentLocation is not MineShaft shaft)
        {
            return;
        }

        // track mine ladders and shafts
        foreach (var tile in shaft.GetLadderTiles())
        {
            tile.TrackWhenOffScreen(Color.Lime);
            if (shouldHighlightOnScreen)
            {
                tile.TrackWhenOnScreen(Color.Lime);
            }
        }
    }
}
