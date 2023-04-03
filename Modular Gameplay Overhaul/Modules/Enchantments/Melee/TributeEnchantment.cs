﻿namespace DaLion.Overhaul.Modules.Enchantments.Melee;

#region using directives

using System.Xml.Serialization;
using StardewValley.Monsters;

#endregion using directives

/// <summary>
///     Attacks that would leave an enemy below 10% max health immediately execute the enemy, converting the remaining health into gold.
///     Consecutive kills without taking damage increase the threshold by 1%.
/// </summary>
[XmlType("Mods_DaLion_TributeEnchantment")]
public sealed class TributeEnchantment : BaseWeaponEnchantment
{
    internal float Threshold { get; set; } = 0.1f;

    /// <inheritdoc />
    public override string GetName()
    {
        return I18n.Get("enchantments.tribute.name");
    }

    /// <inheritdoc />
    protected override void _OnDealDamage(Monster monster, GameLocation location, Farmer who, ref int amount)
    {
        if (!who.IsLocalPlayer)
        {
            return;
        }

        var tribute = (int)((monster.MaxHealth * this.Threshold) - monster.Health);
        if (tribute <= 0)
        {
            return;
        }

        monster.Health = 0;
        who.Money += tribute;
    }

    /// <inheritdoc />
    protected override void _OnMonsterSlay(Monster m, GameLocation location, Farmer who)
    {
        this.Threshold += 0.01f;
    }

    /// <inheritdoc />
    protected override void _OnUnequip(Farmer who)
    {
        base._OnUnequip(who);
        if (who.IsLocalPlayer)
        {
            this.Threshold = 0;
        }
    }
}
