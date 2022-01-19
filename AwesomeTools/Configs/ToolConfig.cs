﻿namespace DaLion.Stardew.Tools.Configs;

#region using directives

using StardewModdingAPI.Utilities;

#endregion using directives

/// <summary>The mod user-defined settings.</summary>
public class ToolConfig
{
    /// <summary>The Axe configuration settings.</summary>
    public AxeConfig AxeConfig { get; set; } = new();

    /// <summary>The Pickaxe configuration settings.</summary>
    public PickaxeConfig PickaxeConfig { get; set; } = new();

    /// <summary>Whether charging requires a mod key to activate.</summary>
    public bool RequireModkey { get; set; } = true;

    /// <summary>The chosen mod key(s).</summary>
    public KeybindList Modkey { get; set; } = KeybindList.Parse("LeftShift, LeftShoulder");

    /// <summary>Whether to show affected tiles overlay while charging.</summary>
    public bool HideAffectedTiles { get; set; } = false;

    /// <summary>How much stamina the shockwave should consume.</summary>
    public float StaminaCostMultiplier { get; set; } = 1.0f;

    /// <summary>Affects the shockwave travel speed. Lower is faster. Set to 0 for instant.</summary>
    public uint TicksBetweenWaves { get; set; } = 4;

    /// <summary>Whether to enable debugging features.</summary>
    public bool EnableDebug { get; set; } = false;
}