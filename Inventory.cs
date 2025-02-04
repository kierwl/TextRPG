using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Textrpg
{
    
    internal class Inventory
    {
        private List<Item> items = new List<Item>();
        private List<Item> equippedItem = new List<Item>();
        private Player player;
        public Inventory(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player), "플레이어 객체가 없습니다.");
            }
            this.player = player;
            items = new List<Item>();
            equippedItem = new List<Item>();
        }

        // 장착된 아이템 목록을 반환하는 메서드
        public List<Item> GetEquippedItems()
        {
            return equippedItem;
        }


        // 아이템 추가
        public void AddItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"{item.Name}을(를) 획득하였습니다.");
        }

        // 아이템 제거
        public void RemoveItem(Item item)
        {
            items.Remove(item);
            Console.WriteLine($"{item.Name}을(를) 사용하였습니다.");
        }

        // 아이템 사용
        public void UseItem(int index)
        {
            if (index < 0 || index >= items.Count)
            {
                Console.WriteLine("잘못된 아이템 번호입니다.");
                return;
            }

            Item item = items[index];

            if (item.Type == ItemType.Potion)
            {
                item.Use(player);
                RemoveItem(item); // 포션은 사용 후 삭제
            }
            else
            {
                Console.WriteLine("이 아이템은 사용할 수 없습니다. 장착해야 합니다.");
            }
        }
        // 장비 착용
        public void EquipItem(int index)
        {
            if (index < 0 || index >= items.Count)
            {
                Console.WriteLine("잘못된 아이템 번호입니다.");
                return;
            }

            Item item = items[index];

            if (item.Type == ItemType.Weapon || item.Type == ItemType.Armor)
            {
                if (!equippedItem.Contains(item))
                {
                    equippedItem.Add(item);
                    item.Equip(player);
                    
                }
                else
                {
                    Console.WriteLine($"{item.Name}은(는) 이미 장착 중입니다.");
                }
            }
            else
            {
                Console.WriteLine("이 아이템은 장착할 수 없습니다.");
            }
        }
        public void UnequipItem(int index)
        {
            if (index < 0 || index >= equippedItem.Count)
            {
                Console.WriteLine("잘못된 아이템 번호입니다.");
                return;
            }

            Item item = equippedItem[index];
            equippedItem.Remove(item);
            item.Unequip(player);
        }

        // 아이템 목록 출력
        public void ShowItems()
        {
            Console.WriteLine("소지품 목록:");
            if (items.Count == 0)
            {
                Console.WriteLine("현재 소지품이 없습니다.");
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    Item item = items[i];

                    string equippedMark = equippedItem.Contains(item) ? "[E] " : "";
                    Console.WriteLine($"{i + 1}. {equippedMark}{item.Name}: {item.Description}");

                }
            }
            Console.WriteLine("\n1. [장착 관리]");
            Console.WriteLine("2. [아이템 사용]");
            Console.WriteLine("0. 돌아가기");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    EquipManager(player);
                    break;
                case 2:
                    Console.Write("사용할 아이템 번호를 입력하세요: ");
                    int useIndex = int.Parse(Console.ReadLine()) - 1;
                    UseItem(useIndex);
                    break;
                case 0:
                    Console.WriteLine("인벤토리에서 나갑니다.");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }

        public void EquipManager(Player player)
        {
            Console.WriteLine("\n** 장착 관리 **");

            if (equippedItem.Count == 0)
            {
                Console.WriteLine("현재 장착된 아이템이 없습니다.");
            }
            else
            {
                for (int i = 0; i < equippedItem.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {equippedItem[i].Name}");
                }
            }

            Console.WriteLine("\n1. 장비 장착");
            Console.WriteLine("2. 장비 해제");
            Console.WriteLine("0. 나가기");

            Console.Write("원하는 행동을 선택하세요: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("장착할 아이템 번호를 입력하세요: ");
                    EquipItem(int.Parse(Console.ReadLine()) - 1);
                    break;
                case 2:
                    Console.Write("해제할 아이템 번호를 입력하세요: ");
                    UnequipItem(int.Parse(Console.ReadLine()) - 1);
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }


        }
    }
}

