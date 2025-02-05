using System;
using System.Collections.Generic;

class Map
{
    private Dictionary<string, Location> locations = new Dictionary<string, Location> // locations 필드 추가
    {
        { "숲", new Location("숲", "나무가 빽빽한 숲이다.", true) },
        { "마을", new Location("마을", "안전한 마을이다.", false) },
        { "던전", new Location("던전", "자격이 되지 않는다면 입장 할 수 없습니다.", false) },
        { "사막", new Location("사막", "매우 뜨거운 사막이다.", true) }
    };

    public void ShowLocations()
    {
        foreach (var loc in locations)
        {
            Console.WriteLine($"- {loc.Key}");
        }
    }

    public Location GetLocation(string name) => locations.ContainsKey(name) ? locations[name] : null; // GetLocation 메서드 추가
}
