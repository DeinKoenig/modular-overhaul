﻿namespace DaLion.Overhaul.Modules.Arsenal.Integrations;

#region using directives

using DaLion.Shared.Extensions.SMAPI;
using DaLion.Shared.Integrations;

#endregion using directives

internal sealed class VanillaTweaksIntegration : BaseIntegration
{
    /// <summary>Initializes a new instance of the <see cref="VanillaTweaksIntegration"/> class.</summary>
    /// <param name="modRegistry">An API for fetching metadata about loaded mods.</param>
    internal VanillaTweaksIntegration(IModRegistry modRegistry)
        : base("VanillaTweaks", "Taiyo.VanillaTweaks", null, modRegistry)
    {
        ModEntry.Integrations[this.ModName] = this;
    }

    /// <summary>Gets the value of the <c>RingsCategoryEnabled</c> config setting.</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:Property summary documentation should match accessors", Justification = "Doesn't make sense in this context.")]
    internal static bool WeaponsCategoryEnabled { get; private set; }

    /// <inheritdoc />
    protected override void RegisterImpl()
    {
        this.AssertLoaded();
        WeaponsCategoryEnabled = ModHelper
            .ReadContentPackConfig("Taiyo.VanillaTweaks")
            ?.Value<bool>("WeaponsCategoryEnabled") == true;
    }
}
