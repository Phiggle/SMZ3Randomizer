﻿using System.Collections.Generic;
using System.Linq;
using TrackerCouncil.Smz3.Shared;
using TrackerCouncil.Smz3.Data.Configuration.ConfigTypes;
using TrackerCouncil.Smz3.Data.Options;
using TrackerCouncil.Smz3.Data.Services;
using TrackerCouncil.Smz3.Shared.Enums;
using TrackerCouncil.Smz3.Shared.Models;

namespace TrackerCouncil.Smz3.Data.WorldData.Regions.Zelda;

public class SkullWoods : Z3Region, IHasReward, IDungeon
{
    public static readonly int[] MusicAddresses = new[] {
        0x02D5BA,
        0x02D5BB,
        0x02D5BC,
        0x02D5BD,
        0x02D608,
        0x02D609,
        0x02D60A,
        0x02D60B
    };

    public SkullWoods(World world, Config config, IMetadataService? metadata, TrackerState? trackerState) : base(world, config, metadata, trackerState)
    {
        RegionItems = new[] { ItemType.KeySW, ItemType.BigKeySW, ItemType.MapSW, ItemType.CompassSW };

        PotPrison = new Location(this, LocationId.SkullWoodsPotPrison, 0x1E9A1, LocationType.Regular,
            name: "Pot Prison",
            vanillaItem: ItemType.KeySW,
            memoryAddress: 0x57,
            memoryFlag: 0x5,
            metadata: metadata,
            trackerState: trackerState);

        CompassChest = new Location(this, LocationId.SkullWoodsCompassChest, 0x1E992, LocationType.Regular,
            name: "Compass Chest",
            vanillaItem: ItemType.CompassSW,
            memoryAddress: 0x67,
            memoryFlag: 0x4,
            metadata: metadata,
            trackerState: trackerState);

        BigChest = new Location(this, LocationId.SkullWoodsBigChest, 0x1E998, LocationType.Regular,
                name: "Big Chest",
                vanillaItem: ItemType.Firerod,
                access: items => items.BigKeySW,
                memoryAddress: 0x58,
                memoryFlag: 0x4,
                metadata: metadata,
                trackerState: trackerState)
            .AlwaysAllow((item, items) => item.Is(ItemType.BigKeySW, World));

        MapChest = new Location(this, LocationId.SkullWoodsMapChest, 0x1E99B, LocationType.Regular,
            name: "Map Chest",
            vanillaItem: ItemType.MapSW,
            memoryAddress: 0x58,
            memoryFlag: 0x5,
            metadata: metadata,
            trackerState: trackerState);

        PinballRoom = new Location(this, LocationId.SkullWoodsPinballRoom, 0x1E9C8, LocationType.Regular,
                name: "Pinball Room",
                vanillaItem: ItemType.KeySW,
                memoryAddress: 0x68,
                memoryFlag: 0x4,
                metadata: metadata,
                trackerState: trackerState)
            .Allow((item, items) => item.Is(ItemType.KeySW, World));

        BigKeyChest = new Location(this, LocationId.SkullWoodsBigKeyChest, 0x1E99E, LocationType.Regular,
            name: "Big Key Chest",
            vanillaItem: ItemType.BigKeySW,
            memoryAddress: 0x57,
            memoryFlag: 0x4,
            metadata: metadata,
            trackerState: trackerState);

        BridgeRoom = new Location(this, LocationId.SkullWoodsBridgeRoom, 0x1E9FE, LocationType.Regular,
            name: "Bridge Room",
            vanillaItem: ItemType.KeySW,
            access: items => items.FireRod,
            memoryAddress: 0x59,
            memoryFlag: 0x4,
            metadata: metadata,
            trackerState: trackerState);

        MothulaReward = new Location(this, LocationId.SkullWoodsMothula, 0x308155, LocationType.Regular,
            name: "Mothula",
            vanillaItem: ItemType.HeartContainer,
            access: items => items.FireRod && items.Sword && items.KeySW >= 3,
            memoryAddress: 0x29,
            memoryFlag: 0xB,
            metadata: metadata,
            trackerState: trackerState);

        MemoryAddress = 0x29;
        MemoryFlag = 0xB;
        StartingRooms = new List<int> { 86, 87, 88, 89, 103, 104 };
        Metadata = metadata?.Region(GetType()) ?? new RegionInfo("Skull Woods");
        DungeonMetadata = metadata?.Dungeon(GetType()) ?? new DungeonInfo("Skull Woods", "Mothula");
        DungeonState = trackerState?.DungeonStates.First(x => x.WorldId == world.Id && x.Name == GetType().Name) ?? new TrackerDungeonState();
        Reward = new Reward(DungeonState.Reward ?? RewardType.None, world, this, metadata, DungeonState);
        MapName = "Dark World";
    }

    public override string Name => "Skull Woods";

    public RewardType DefaultRewardType => RewardType.CrystalBlue;

    public override List<string> AlsoKnownAs { get; }
        = new List<string>() { "Skill Woods" };

    public int SongIndex { get; init; } = 6;
    public string Abbreviation => "SW";
    public LocationId? BossLocationId => LocationId.SkullWoodsMothula;

    public Reward Reward { get; set; }

    public DungeonInfo DungeonMetadata { get; set; }

    public TrackerDungeonState DungeonState { get; set; }

    public Region ParentRegion => World.DarkWorldNorthWest;

    public Location PotPrison { get; }

    public Location CompassChest { get; }

    public Location BigChest { get; }

    public Location MapChest { get; }

    public Location PinballRoom { get; }

    public Location BigKeyChest { get; }

    public Location BridgeRoom { get; }

    public Location MothulaReward { get; }

    public override bool CanEnter(Progression items, bool requireRewards)
    {
        return items.MoonPearl && World.DarkWorldNorthWest.CanEnter(items, requireRewards);
    }

    public bool CanComplete(Progression items)
    {
        return MothulaReward.IsAvailable(items);
    }
}
