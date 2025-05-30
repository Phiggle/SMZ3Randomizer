﻿using System.Collections.Generic;
using static Randomizer.SMZ3.LegacyRewardType;

namespace Randomizer.SMZ3.Regions.Zelda.LightWorld {

    class LegacyNorthEast : LegacyZ3Region {

        public override string Name => "Light World North East";
        public override string Area => "Light World";

        public LegacyNorthEast(LegacyWorld legacyWorld, LegacyConfig legacyConfig) : base(legacyWorld, legacyConfig) {
            var sphereOne = -10;
            Locations = new List<LegacyLocation> {
                new LegacyLocation(this, 256+36, 0x1DE1C3, LocationType.Regular, "King Zora",
                    items => items.CanLiftLight() || items.Flippers),
                new LegacyLocation(this, 256+37, 0x308149, LocationType.Regular, "Zora's Ledge",
                    items => items.Flippers),
                new LegacyLocation(this, 256+254, 0x1E9B0, LocationType.Regular, "Waterfall Fairy - Left",
                    items => items.Flippers),
                new LegacyLocation(this, 256+39, 0x1E9D1, LocationType.Regular, "Waterfall Fairy - Right",
                    items => items.Flippers),
                new LegacyLocation(this, 256+40, 0x308014, LocationType.Regular, "Potion Shop",
                    items => items.Mushroom),
                new LegacyLocation(this, 256+41, 0x1EA82, LocationType.Regular, "Sahasrahla's Hut - Left").Weighted(sphereOne),
                new LegacyLocation(this, 256+42, 0x1EA85, LocationType.Regular, "Sahasrahla's Hut - Middle").Weighted(sphereOne),
                new LegacyLocation(this, 256+43, 0x1EA88, LocationType.Regular, "Sahasrahla's Hut - Right").Weighted(sphereOne),
                new LegacyLocation(this, 256+44, 0x5F1FC, LocationType.Regular, "Sahasrahla",
                    items => LegacyWorld.CanAcquire(items, PendantGreen)),
            };
        }

    }

}
