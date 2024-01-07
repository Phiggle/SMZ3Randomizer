﻿using System.Collections.Generic;
using Randomizer.Data.Configuration.ConfigTypes;
using Randomizer.Shared;
using Randomizer.Data.Options;
using Randomizer.Data.Services;
using Randomizer.Shared.Models;

namespace Randomizer.Data.WorldData.Regions.Zelda.DarkWorld
{
    public class DarkWorldMire : Z3Region
    {
        public DarkWorldMire(World world, Config config, IMetadataService? metadata, TrackerState? trackerState) : base(world, config, metadata, trackerState)
        {
            MireShed = new MireShedRoom(this, metadata, trackerState);

            StartingRooms = new List<int>() { 112 };
            IsOverworld = true;
            Metadata = metadata?.Region(GetType()) ?? new RegionInfo("Dark World Mire");
            MapName = "Dark World";
        }

        public override string Name => "Dark World Mire";

        public override string Area => "Dark World";

        public MireShedRoom MireShed { get; }

        public override bool CanEnter(Progression items, bool requireRewards)
        {
            return (items.Flute && Logic.CanLiftHeavy(items)) || Logic.CanAccessMiseryMirePortal(items);
        }

        public class MireShedRoom : Room
        {
            public MireShedRoom(Region region, IMetadataService? metadata, TrackerState? trackerState)
                : base(region, "Mire Shed", metadata)
            {
                Locations = new List<Location>
                {
                    new Location(this, LocationId.MireShedLeft, 0x1EA73, LocationType.Regular,
                        name: "Mire Shed - Left",
                        vanillaItem: ItemType.HeartPiece,
                        access: items => items.MoonPearl,
                        memoryAddress: 0x10D,
                        memoryFlag: 0x4,
                        metadata: metadata,
                        trackerState: trackerState),
                    new Location(this, LocationId.MireShedRight, 0x1EA76, LocationType.Regular,
                        name: "Mire Shed - Right",
                        vanillaItem: ItemType.TwentyRupees,
                        access: items => items.MoonPearl,
                        memoryAddress: 0x10D,
                        memoryFlag: 0x5,
                        metadata: metadata,
                        trackerState: trackerState)
                };
            }
        }
    }
}
