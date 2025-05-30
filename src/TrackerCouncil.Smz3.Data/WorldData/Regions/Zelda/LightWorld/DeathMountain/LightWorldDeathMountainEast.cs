﻿using System.Collections.Generic;
using TrackerCouncil.Smz3.Shared;
using TrackerCouncil.Smz3.Data.Configuration.ConfigTypes;
using TrackerCouncil.Smz3.Data.Options;
using TrackerCouncil.Smz3.Data.Services;
using TrackerCouncil.Smz3.Shared.Enums;
using TrackerCouncil.Smz3.Shared.Models;

namespace TrackerCouncil.Smz3.Data.WorldData.Regions.Zelda.LightWorld.DeathMountain;

public class LightWorldDeathMountainEast : Z3Region
{
    public LightWorldDeathMountainEast(World world, Config config, IMetadataService? metadata, TrackerState? trackerState) : base(world, config, metadata, trackerState)
    {
        FloatingIsland = new Location(this, LocationId.FloatingIsland, 0x308141, LocationType.Regular,
            name: "Floating Island",
            vanillaItem: ItemType.HeartPiece,
            access: items => items.Mirror && World.Logic.CanNavigateDarkWorld(items) && Logic.CanLiftHeavy(items),
            memoryAddress: 0x5,
            memoryFlag: 0x40,
            memoryType: LocationMemoryType.ZeldaMisc,
            metadata: metadata,
            trackerState: trackerState);

        SpiralCave = new Location(this, LocationId.SpiralCave, 0x1E9BF, LocationType.Regular,
            name: "Spiral Cave",
            vanillaItem: ItemType.FiftyRupees,
            memoryAddress: 0xFE,
            memoryFlag: 0x4,
            metadata: metadata,
            trackerState: trackerState);

        MirrorCave = new Location(this, LocationId.MimicCave, 0x1E9C5, LocationType.Regular,
            name: "Mimic Cave",
            vanillaItem: ItemType.HeartPiece,
            access: items => items.Mirror && items.KeyTR >= 2 && World.TurtleRock.CanEnter(items, true),
            memoryAddress: 0x10C,
            memoryFlag: 0x4,
            trackerLogic: items => items.HasMarkedMedallion(World.TurtleRock.PrerequisiteState.MarkedItem),
            metadata: metadata,
            trackerState: trackerState);

        ParadoxCave = new ParadoxCaveRoom(this, metadata, trackerState);

        StartingRooms = new List<int>() { 5, 7 };
        IsOverworld = true;
        Metadata = metadata?.Region(GetType()) ?? new RegionInfo("Light World Death Mountain West");
        MapName = "Light World";
    }

    public override string Name => "Light World Death Mountain East";

    public override string Area => "Death Mountain";

    public Location FloatingIsland { get; }

    public Location SpiralCave { get; }

    public Location MirrorCave { get; }

    public ParadoxCaveRoom ParadoxCave { get; }


    public override bool CanEnter(Progression items, bool requireRewards)
    {
        return World.LightWorldDeathMountainWest.CanEnter(items, requireRewards) && (
            (items.Hammer && items.Mirror) ||
            items.Hookshot
        );
    }

    public class ParadoxCaveRoom : Room
    {
        public ParadoxCaveRoom(Region region, IMetadataService? metadata, TrackerState? trackerState)
            : base(region, "Paradox Cave", metadata)
        {
            Locations = new List<Location>
            {
                new Location(this, LocationId.ParadoxCaveUpperLeft, 0x1EB39, LocationType.Regular,
                    name: "Paradox Cave Upper - Left",
                    vanillaItem: ItemType.ThreeBombs,
                    memoryAddress: 0xFF,
                    memoryFlag: 0x4,
                    metadata: metadata,
                    trackerState: trackerState),
                new Location(this, LocationId.ParadoxCaveUpperRight, 0x1EB3C, LocationType.Regular,
                    name: "Paradox Cave Upper - Right",
                    vanillaItem: ItemType.TenArrows,
                    memoryAddress: 0xFF,
                    memoryFlag: 0x5,
                    metadata: metadata,
                    trackerState: trackerState),
                new Location(this, LocationId.ParadoxCaveLowerFarLeft, 0x1EB2A, LocationType.Regular,
                    name: "Paradox Cave Lower - Far Left",
                    vanillaItem: ItemType.TwentyRupees,
                    memoryAddress: 0xEF,
                    memoryFlag: 0x4,
                    metadata: metadata,
                    trackerState: trackerState),
                new Location(this, LocationId.ParadoxCaveLowerLeft, 0x1EB2D, LocationType.Regular,
                    name: "Paradox Cave Lower - Left",
                    vanillaItem: ItemType.TwentyRupees,
                    memoryAddress: 0xEF,
                    memoryFlag: 0x5,
                    metadata: metadata,
                    trackerState: trackerState),
                new Location(this, LocationId.ParadoxCaveLowerMiddle, 0x1EB36, LocationType.Regular,
                    name: "Paradox Cave Lower - Middle",
                    vanillaItem: ItemType.TwentyRupees,
                    memoryAddress: 0xEF,
                    memoryFlag: 0x8,
                    metadata: metadata,
                    trackerState: trackerState),
                new Location(this, LocationId.ParadoxCaveLowerRight, 0x1EB30, LocationType.Regular,
                    name: "Paradox Cave Lower - Right",
                    vanillaItem: ItemType.TwentyRupees,
                    memoryAddress: 0xEF,
                    memoryFlag: 0x6,
                    metadata: metadata,
                    trackerState: trackerState),
                new Location(this, LocationId.ParadoxCaveLowerFarRight, 0x1EB33, LocationType.Regular,
                    name: "Paradox Cave Lower - Far Right",
                    vanillaItem: ItemType.TwentyRupees,
                    memoryAddress: 0xEF,
                    memoryFlag: 0x7,
                    metadata: metadata,
                    trackerState: trackerState)
            };
        }
    }
}
