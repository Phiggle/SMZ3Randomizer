﻿using System;
using System.Linq;
using TrackerCouncil.Smz3.Data.WorldData;

namespace TrackerCouncil.Smz3.Data.Configuration.ConfigTypes;

/// <summary>
/// Representation of a location to be displayed on the map at a certain position
/// </summary>
public class TrackerMapLocation
{
    /// <summary>
    /// Initializes a new instance of the <see
    /// cref="TrackerMapLocation"/> class using the specified JSON file
    /// name.
    /// </summary>
    /// <param name="type">The type of location this is</param>
    /// <param name="region">The region that this location belongs to</param>
    /// <param name="regionTypeName">The name of the C# type for the region</param>
    /// <param name="name">The name of this location</param>
    /// <param name="x">The x location to place this location on the map</param>
    /// <param name="y">The y location to place this location on the map</param>
    /// <param name="scale">The ratio at which this location has been scaled down (for combined maps)</param>
    public TrackerMapLocation(MapLocationType type, string region, string regionTypeName, string name, int x, int y, double scale = 1)
    {
        Type = type;
        Region = region;
        RegionTypeName = regionTypeName;
        Name = name;
        X = x;
        Y = y;
        Scale = scale;
    }

    /// <summary>
    /// The region that this location belongs to
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// Full class type name for the region
    /// </summary>
    public string RegionTypeName { get; set; }

    /// <summary>
    /// The name of this location
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The x location to place this location on the map
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// The y location to place this location on the map
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// The ratio at which this location has been scaled down (for combined maps)
    /// </summary>
    public double Scale { get; set; }

    public MapLocationType Type { get; set; }

    /// <summary>
    /// Given a randomizer <see cref="Location"/>, see if this location matches it by name
    /// </summary>
    /// <param name="loc">The randomizer location to compare to</param>
    /// <returns>True if the location matches the randomizer location, false otherwise</returns>
    public bool MatchesSMZ3Location(Location loc)
    {
        return RegionTypeName == loc.Region.GetType().FullName &&
               (Name == loc.Name || Name == loc.Room?.Name || Region == Name);
    }

    public enum MapLocationType
    {
        Item,
        Boss,
        SMDoor
    }

    public string GetName(World world)
    {
        var room = world.Rooms.SingleOrDefault(x => x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
        if (room?.Metadata != null)
            return room.Metadata.Name?[0] ?? Name;

        var dungeon = world.Regions.SingleOrDefault(x => x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
        if (dungeon?.Metadata != null)
            return dungeon.Metadata.Name?[0] ?? Name;

        var location = world.Locations.SingleOrDefault(x => x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
        if (location?.Metadata != null)
            return location.Metadata.Name?[0] ?? Name;

        return Name;
    }
}
