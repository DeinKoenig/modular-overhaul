﻿namespace DaLion.Overhaul.Modules.Enchantments.Patchers;

#region using directives

using System.Collections.Generic;
using System.Linq;
using DaLion.Overhaul.Modules.Enchantments.Melee;
using DaLion.Shared.Harmony;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;

#endregion using directives

[UsedImplicitly]
internal sealed class BuffsDisplayDrawPatcher : HarmonyPatcher
{
    /// <summary>Initializes a new instance of the <see cref="BuffsDisplayDrawPatcher"/> class.</summary>
    internal BuffsDisplayDrawPatcher()
    {
        this.Target = this.RequireMethod<BuffsDisplay>(nameof(BuffsDisplay.draw), new[] { typeof(SpriteBatch) });
    }

    /// <summary>Patch to draw Energized buff.</summary>
    [HarmonyPostfix]
    private static void BuffsDisplayDrawPostfix(Dictionary<ClickableTextureComponent, Buff> ___buffs, SpriteBatch b)
    {
        var energized = (Game1.player.CurrentTool as MeleeWeapon)?.GetEnchantmentOfType<EnergizedEnchantment>();
        if (energized is not null)
        {
            var (clickableTextureComponent, buff) =
                ___buffs.FirstOrDefault(p => p.Value.which == (Manifest.UniqueID + "Energized").GetHashCode());
            if ((clickableTextureComponent, buff) == default)
            {
                return;
            }

            var counter = energized.Energy;
            b.DrawString(
                Game1.tinyFont,
                counter.ToString(),
                new Vector2(
                    clickableTextureComponent.bounds.Right - (counter >= 10 ? counter >= 100 ? 24 : 16 : 8),
                    clickableTextureComponent.bounds.Bottom - 24),
                counter >= EnergizedEnchantment.MaxEnergy ? Color.Yellow : Color.White);
            return;
        }

        var explosive = (Game1.player.CurrentTool as MeleeWeapon)?.GetEnchantmentOfType<ExplosiveEnchantment>();
        if (explosive is not null)
        {
            var (clickableTextureComponent, buff) =
                ___buffs.FirstOrDefault(p => p.Value.which == (Manifest.UniqueID + "Explosive").GetHashCode());
            if ((clickableTextureComponent, buff) == default)
            {
                return;
            }

            var counter = explosive.ExplosionRadius;
            b.DrawString(
                Game1.tinyFont,
                counter.ToString(),
                new Vector2(
                    clickableTextureComponent.bounds.Right - 8,
                    clickableTextureComponent.bounds.Bottom - 24),
                counter >= explosive.MaxRadius ? Color.Yellow : Color.White);
        }
    }
}
