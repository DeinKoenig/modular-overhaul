﻿namespace DaLion.Ligo.Modules.Arsenal.Common.Enchantments;

#region using directives

using DaLion.Ligo.Modules.Arsenal.Common.Projectiles;
using DaLion.Shared.Enums;
using Microsoft.Xna.Framework;
using StardewValley.Tools;

#endregion using directives

/// <summary>The secondary <see cref="BaseWeaponEnchantment"/> which characterizes Infinity weapons.</summary>
public class InfinityEnchantment : BaseWeaponEnchantment
{
    /// <inheritdoc />
    public override bool IsSecondaryEnchantment()
    {
        return true;
    }

    /// <inheritdoc />
    public override bool IsForge()
    {
        return false;
    }

    /// <inheritdoc />
    public override int GetMaximumLevel()
    {
        return 1;
    }

    /// <inheritdoc />
    public override bool ShouldBeDisplayed()
    {
        return false;
    }

    /// <inheritdoc />
    protected override void _OnSwing(MeleeWeapon weapon, Farmer farmer)
    {
        base._OnSwing(weapon, farmer);
        if (farmer.health < farmer.maxHealth)
        {
            return;
        }

        var facingDirection = (FacingDirection)farmer.FacingDirection;
        var facingVector = facingDirection.ToVector();
        var startingPosition = farmer.getStandingPosition() + (facingVector * 64f) - new Vector2(32f, 32f);
        var velocity = facingVector * 10f;
        var rotation = (float)Math.PI / 180f * 32f;
        farmer.currentLocation.projectiles.Add(new InfinityProjectile(
            weapon,
            farmer,
            startingPosition,
            velocity.X,
            velocity.Y,
            rotation));
    }
}
