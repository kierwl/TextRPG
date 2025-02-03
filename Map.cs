using System;
using System.Collections.Generic;

class Map
{
    private Dictionary<string, Location> locations = new Dictionary<string, Location>
    {
        { "½£", new Location("½£", "³ª¹«°¡ »ª»ªÇÑ ½£ÀÌ´Ù.", true) },
        { "¸¶À»", new Location("¸¶À»", "¾ÈÀüÇÑ ¸¶À»ÀÌ´Ù.", false) }
    };

    public void ShowLocations()
    {
        foreach (var loc in locations)
        {
            Console.WriteLine($"- {loc.Key}");
        }
    }

    public Location GetLocation(string name) => locations.ContainsKey(name) ? locations[name] : null;
}
