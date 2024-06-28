﻿using System.Collections.Generic;
using TrackerCouncil.Smz3.Shared;
using TrackerCouncil.Smz3.Data.Configuration.ConfigTypes;
using TrackerCouncil.Smz3.Data.Options;
using TrackerCouncil.Smz3.Data.Services;
using TrackerCouncil.Smz3.Shared.Enums;
using TrackerCouncil.Smz3.Shared.Models;

namespace TrackerCouncil.Smz3.Data.WorldData.Regions.Zelda.DarkWorld;

public class DarkWorldNorthWest : Z3Region
{
    public DarkWorldNorthWest(World world, Config config, IMetadataService? metadata, TrackerState? trackerState) : base(world, config, metadata, trackerState)
    {
        BumperCaveLedge = new Location(this, LocationId.BumperCave, 0x308146, LocationType.Regular,
            name: "Bumper Cave",
            vanillaItem: ItemType.HeartPiece,
            access: items => Logic.CanLiftLight(items) && items.Cape,
            memoryAddress: 0x4A,
            memoryFlag: 0x40,
            memoryType: LocationMemoryType.ZeldaMisc,
            metadata: metadata,
            trackerState: trackerState);

        ChestGame = new Location(this, LocationId.ChestGame, 0x1EDA8, LocationType.Regular,
            name: "Chest Game",
            vanillaItem: ItemType.HeartPiece,
            memoryAddress: 0x106,
            memoryFlag: 0xA,
            metadata: metadata,
            trackerState: trackerState);

        CShapedHouse = new Location(this, LocationId.CShapedHouse, 0x1E9EF, LocationType.Regular,
            name: "C-Shaped House", // ???
            vanillaItem: ItemType.ThreeHundredRupees, // ???
            memoryAddress: 0x11C,
            memoryFlag: 0x4,
            metadata: metadata,
            trackerState: trackerState);

        Brewery = new Location(this, LocationId.Brewery, 0x1E9EC, LocationType.Regular,
            name: "Brewery", // ???
            vanillaItem: ItemType.ThreeHundredRupees,
            memoryAddress: 0x106,
            memoryFlag: 0x4,
            metadata: metadata,
            trackerState: trackerState);

        PegWorld = new Location(this, LocationId.HammerPegs, 0x308006, LocationType.Regular,
            name: "Hammer Pegs",
            vanillaItem: ItemType.HeartPiece,
            access: items => Logic.CanLiftHeavy(items) && items.Hammer,
            memoryAddress: 0x127,
            memoryFlag: 0xA,
            metadata: metadata,
            trackerState: trackerState);

        PurpleChestTurnin = new Location(this, LocationId.PurpleChest, 0x6BD68, LocationType.Regular,
            name: "Purple Chest",
            vanillaItem: ItemType.Bottle,
            access: items => Logic.CanLiftHeavy(items),
            memoryAddress: 0x149,
            memoryFlag: 0x10,
            memoryType: LocationMemoryType.ZeldaMisc,
            metadata: metadata,
            trackerState: trackerState);

        StartingRooms = new List<int>() { 64, 66, 74, 80, 81, 82, 83, 84, 88, 90, 98 };
        IsOverworld = true;
        Metadata = metadata?.Region(GetType()) ?? new RegionInfo("Dark World North West");
        MapName = "Dark World";
    }

    public override string Name => "Dark World North West";
    public override string Area => "Dark World";

    public Location BumperCaveLedge { get; }

    public Location ChestGame { get; }

    public Location CShapedHouse { get; }

    public Location Brewery { get; }

    public Location PegWorld { get; }

    public Location PurpleChestTurnin { get; }

    public override bool CanEnter(Progression items, bool requireRewards)
    {
        return items.MoonPearl && (((
                                       Logic.CheckAgahnim(items, World, requireRewards) ||
                                       (Logic.CanAccessDarkWorldPortal(items) && items.Flippers)
                                   ) && items.Hookshot && (items.Flippers || Logic.CanLiftLight(items) || items.Hammer)) ||
                                   (items.Hammer && Logic.CanLiftLight(items)) ||
                                   Logic.CanLiftHeavy(items)
            );
    }

}
