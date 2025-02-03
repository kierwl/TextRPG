using System;
using System.Collections.Generic;

class Map
{
    private Dictionary<string, Location> locations = new Dictionary<string, Location>
    {
        { "��", new Location("��", "������ ������ ���̴�.", true) },
        { "����", new Location("����", "������ �����̴�.", false) }
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
