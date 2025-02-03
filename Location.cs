using System;

class Location
{
    public string Name { get; }
    public string Description { get; }
    public bool HasEnemy { get; }

    public Location(string name, string description, bool hasEnemy)
    {
        Name = name;
        Description = description;
        HasEnemy = hasEnemy;
    }
}
