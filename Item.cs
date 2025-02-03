using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textrpg
{
    enum ItemType { Weapon, Armor, Potion }
    internal class Item
    {
        public string Name { get; }
        public string Description { get; }
        public ItemType Type { get; }
        public int Value { get; }

        public Item(string name, string description, ItemType type, int value)
        {
            Name = name;
            Description = description;
            Type = type;
            Value = value;
        }

        public void Use(Player player)
        {
            switch (Type)
            {
                case ItemType.Weapon:
                    player.AttackPower += Value;
                    Console.WriteLine($"{player.Name}의 공격력이 {Value} 증가했습니다.");
                    break;
                case ItemType.Armor:
                    player.Defense += Value;
                    Console.WriteLine($"{player.Name}의 방어력이 {Value} 증가했습니다.");
                    break;
                case ItemType.Potion:
                    player.Health += Value;
                    Console.WriteLine($"{player.Name}의 체력이 {Value} 회복되었습니다.");
                    break;
            }
        }
    }
}
