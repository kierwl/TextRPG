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
        public int AttackValue { get; }
        public int DefenseValue { get; }
        public int HealthValue { get; }
        public int SpeedValue { get; }
        
        public Item(string name, string description, ItemType type, int atkvalue, int defvalue, int spdvalue, int hpvalue)
        {
            Name = name;
            Description = description;
            Type = type;
            AttackValue = atkvalue;
            DefenseValue = defvalue;
            SpeedValue = spdvalue;
            HealthValue = hpvalue;
            
        }
        
        public void Use(Player player)
        {
            if (player == null)
            {

                return;
            }

            if (Type == ItemType.Potion)
            {
                player.Health += HealthValue;
                Console.WriteLine($"{player.Name}의 최대체력이 {HealthValue} 증가되었습니다.");
            }
            else
            {
                
                Console.WriteLine("사용 할 수 있는 아이템이 아닙니다.");
            }
        }

            // 장비 착용 (무기, 방어구)
        public void Equip(Player player)
        {
            if (player == null)
            {
                
                return;
            }

            if (Type == ItemType.Weapon)
            {
                //player.AttackPower += AttackValue;
                Console.WriteLine($"{Name}을(를) 장착하여 공격력이 {AttackValue} 증가했습니다.");

            }
            else if (Type == ItemType.Armor)
            {
                
                Console.WriteLine($"{Name}을(를) 장착하여 방어력이 {DefenseValue} 증가했습니다.");
               
                Console.WriteLine($"{Name}을(를) 장착하여 방어력이 {HealthValue} 증가했습니다.");
            }
            else
            {
                Console.WriteLine("이 아이템은 장착할 수 없습니다.");
            }
        }
        // 장비 해제 (무기, 방어구)
        public void Unequip(Player player)
        {
            if (player == null)
            {

                return;
            }

            if (Type == ItemType.Weapon)
            {
                
                Console.WriteLine($"{Name}을(를) 해제하여 공격력이 {AttackValue} 감소했습니다.");
            }
            else if (Type == ItemType.Armor)
            {
                
                Console.WriteLine($"{Name}을(를) 해제하여 방어력이 {DefenseValue} 감소했습니다.");
            }
            else
            {
                Console.WriteLine("이 아이템은 장착할 수 없습니다.");
            }
        }
    }
}

