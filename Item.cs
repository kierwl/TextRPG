using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public int Price { get; set; } // 아이템 가격
        public bool IsPurchased { get; set; } // 아이템 구매 여부


        public Item(string name, string description, ItemType type, int atkvalue, int defvalue, int spdvalue, int hpvalue, int price)
        {
            Name = name;
            Description = description;
            Type = type;
            AttackValue = atkvalue;
            DefenseValue = defvalue;
            SpeedValue = spdvalue;
            HealthValue = hpvalue;
            Price = price;
            IsPurchased = false;

        }
        // 📌 복사 생성자 추가
        public Item(Item other)
        {
            Name = other.Name;
            Description = other.Description;
            Type = other.Type;
            AttackValue = other.AttackValue;
            DefenseValue = other.DefenseValue;
            SpeedValue = other.SpeedValue;
            HealthValue = other.HealthValue;
            Price = other.Price;
            IsPurchased = false; // 새로 생성된 아이템은 구매되지 않은 상태로 설정
        }

        public void Purchase(Player player)
        {
            if (player == null)
            {
                Console.WriteLine("플레이어가 없습니다.");
                return;
            }
            if (player.Gold < Price)
            {
                Console.WriteLine("골드가 부족합니다.");
                return;
            }
            player.Gold -= Price;
            IsPurchased = true;
            Console.WriteLine($"{Name}을(를) 구매하였습니다.");
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

