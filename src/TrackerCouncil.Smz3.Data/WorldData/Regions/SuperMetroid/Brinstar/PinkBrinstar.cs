﻿using System.Collections.Generic;
using TrackerCouncil.Shared;
using TrackerCouncil.Smz3.Data.Configuration.ConfigTypes;
using TrackerCouncil.Smz3.Data.Options;
using TrackerCouncil.Smz3.Data.Services;
using TrackerCouncil.Smz3.Shared.Enums;
using TrackerCouncil.Smz3.Shared.Models;

namespace TrackerCouncil.Smz3.Data.WorldData.Regions.SuperMetroid.Brinstar;

public class PinkBrinstar : SMRegion
{
    public PinkBrinstar(World world, Config config, IMetadataService? metadata, TrackerState? trackerState) : base(world, config, metadata, trackerState)
    {
        Weight = -4;
        BigPink = new BigPinkRoom(this, metadata, trackerState);
        PinkBrinstarPowerBomb = new PinkBrinstarPowerBombRoom(this, metadata, trackerState);
        Hoptank = new HoptankRoom(this, metadata, trackerState);
        SporeSpawnSuper = new SporeSpawnSuperRoom(this, metadata, trackerState);
        WaterwayEnergyTank = new WaterwayEnergyTankRoom(this, metadata, trackerState);
        GreenHillZone = new GreenHillZoneRoom(this, metadata, trackerState);
        MemoryRegionId = 1;
        Metadata = metadata?.Region(GetType()) ?? new RegionInfo("Pink Brinstar");
        MapName = "Brinstar";
    }

    public override string Name => "Pink Brinstar";

    public override string Area => "Brinstar";

    public BigPinkRoom BigPink { get; }

    public PinkBrinstarPowerBombRoom PinkBrinstarPowerBomb { get; }

    public HoptankRoom Hoptank { get; }

    public SporeSpawnSuperRoom SporeSpawnSuper { get; }

    public WaterwayEnergyTankRoom WaterwayEnergyTank { get; }

    public GreenHillZoneRoom GreenHillZone { get; }

    public override bool CanEnter(Progression items, bool requireRewards) =>
        (Logic.CanOpenRedDoors(items) && (Logic.CanDestroyBombWalls(items) || Logic.CanParlorSpeedBoost(items))) ||
        Logic.CanUsePowerBombs(items) ||
        (Logic.CanAccessNorfairUpperPortal(items) && items.Morph && items.Wave &&
         (items.Ice || items.HiJump || items.SpaceJump));

    public class BigPinkRoom : Room
    {
        public BigPinkRoom(PinkBrinstar region, IMetadataService? metadata, TrackerState? trackerState)
            : base(region, "Big Pink", metadata)
        {
            Locations = new List<Location>
            {
                new Location(this, LocationId.PinkBrinstarPinkShaftTop, 0x8F8608, LocationType.Visible,
                    name: "Missile (pink Brinstar top)",
                    vanillaItem: ItemType.Missile,
                    access: items => items.Grapple || Logic.CanWallJump(WallJumpDifficulty.Easy) || Logic.CanFly(items),
                    memoryAddress: 0x2,
                    memoryFlag: 0x20,
                    metadata: metadata,
                    trackerState: trackerState),
                new Location(this, LocationId.PinkBrinstarPinkShaftBottom, 0x8F860E, LocationType.Visible,
                    name: "Missile (pink Brinstar bottom)",
                    vanillaItem: ItemType.Missile,
                    memoryAddress: 0x2,
                    memoryFlag: 0x40,
                    metadata: metadata,
                    trackerState: trackerState),
                new Location(this, LocationId.PinkBrinstarPinkShaftChozo, 0x8F8614, LocationType.Chozo,
                    name: "Charge Beam",
                    vanillaItem: ItemType.Charge,
                    access: items => Logic.CanPassBombPassages(items),
                    memoryAddress: 0x2,
                    memoryFlag: 0x80,
                    metadata: metadata,
                    trackerState: trackerState)
            };
        }
    }

    public class PinkBrinstarPowerBombRoom : Room
    {
        public PinkBrinstarPowerBombRoom(PinkBrinstar region, IMetadataService? metadata, TrackerState? trackerState)
            : base(region, "Pink Brinstar Power Bomb Room", metadata)
        {
            Locations = new List<Location>
            {
                new Location(this, LocationId.PinkBrinstarPowerBomb, 0x8F865C, LocationType.Visible,
                    name: "Power Bomb (pink Brinstar)",
                    vanillaItem: ItemType.PowerBomb,
                    access: items => Logic.CanUsePowerBombs(items) && items.Super && Logic.HasEnergyReserves(items, 1)
                                     && (items.Grapple || Logic.CanWallJump(WallJumpDifficulty.Easy) || Logic.CanFly(items)),
                    memoryAddress: 0x3,
                    memoryFlag: 0x1,
                    metadata: metadata,
                    trackerState: trackerState)
            };
        }
    }

    public class HoptankRoom : Room
    {
        public HoptankRoom(PinkBrinstar region, IMetadataService? metadata, TrackerState? trackerState)
            : base(region, "Hoptank Room", metadata)
        {
            Locations = new List<Location>
            {
                new Location(this, LocationId.PinkBrinstarHoptank, 0x8F8824, LocationType.Visible,
                    name: "Energy Tank, Brinstar Gate",
                    vanillaItem: ItemType.ETank,
                    access: items => items.CardBrinstarL2 && Logic.CanUsePowerBombs(items)
                                                          && items.Wave && Logic.HasEnergyReserves(items, 1)
                                                          && (items.Grapple || Logic.CanWallJump(WallJumpDifficulty.Easy) || Logic.CanFly(items)),
                    memoryAddress: 0x4,
                    memoryFlag: 0x8,
                    metadata: metadata,
                    trackerState: trackerState)
            };
        }
    }

    public class SporeSpawnSuperRoom : Room
    {
        public SporeSpawnSuperRoom(PinkBrinstar region, IMetadataService? metadata, TrackerState? trackerState)
            : base(region, "Spore Spawn Super Room", metadata)
        {
            Locations = new List<Location>
            {
                new Location(this, LocationId.PinkBrinstarSporeSpawnSuper, 0x8F84E4, LocationType.Chozo,
                    name: "Super Missile (pink Brinstar)",
                    vanillaItem: ItemType.Super,
                    access: items => items.CardBrinstarBoss && Logic.CanPassBombPassages(items) && items.Super,
                    memoryAddress: 0x1,
                    memoryFlag: 0x40,
                    metadata: metadata,
                    trackerState: trackerState)
            };
        }
    }

    public class WaterwayEnergyTankRoom : Room
    {
        public WaterwayEnergyTankRoom(PinkBrinstar region, IMetadataService? metadata, TrackerState? trackerState)
            : base(region, "Waterway Energy Tank Room", metadata)
        {
            Locations = new List<Location>
            {
                new Location(this, LocationId.PinkBrinstarWaterwayEnergyTank, 0x8F87FA, LocationType.Visible,
                    name: "Energy Tank, Waterway",
                    vanillaItem: ItemType.ETank,
                    access: items => Logic.CanUsePowerBombs(items) && Logic.CanOpenRedDoors(items) && items.SpeedBooster &&
                                     ((Logic.HasEnergyReserves(items, 1) && !World.Config.LogicConfig.WaterwayNeedsGravitySuit) || items.Gravity),
                    memoryAddress: 0x4,
                    memoryFlag: 0x2,
                    metadata: metadata,
                    trackerState: trackerState)
            };
        }
    }

    public class GreenHillZoneRoom : Room
    {
        public GreenHillZoneRoom(PinkBrinstar region, IMetadataService? metadata, TrackerState? trackerState)
            : base(region, "Green Hill Zone", metadata)
        {
            Locations = new List<Location>
            {
                new Location(this, LocationId.GreenBrinstarGreenHillZone, 0x8F8676, LocationType.Visible,
                    name: "Missile (green Brinstar pipe)",
                    vanillaItem: ItemType.Missile,
                    access: items => items.Morph
                                     && (items.PowerBomb || items.Super || Logic.CanAccessNorfairUpperPortal(items))
                                     && (items.HiJump || Logic.CanWallJump(WallJumpDifficulty.Easy) || Logic.CanFly(items)),
                    memoryAddress: 0x3,
                    memoryFlag: 0x2,
                    metadata: metadata,
                    trackerState: trackerState)
            };
        }
    }
}
