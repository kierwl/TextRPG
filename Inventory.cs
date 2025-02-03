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
        private Player player;
        public Inventory()
        {
            items = new List<Item>();
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
        public void UseItem(int index, Player player)
        {
            if (index >= 0 && index < items.Count)
            {
                Item item = items[index];
                item.Use(player);  // 아이템 사용
                RemoveItem(item);  // 사용한 아이템은 제거
            }
            else
            {
                Console.WriteLine("잘못된 아이템 번호입니다.");
            }
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
                    Console.WriteLine($"{i + 1}. {item.Name}: {item.Description}");
                }
            }
        }

        public void EquipManger(Player player)
        {
            Console.WriteLine("\n[장착 관리]");
            Console.WriteLine("1. 장착 아이템 확인");
            Console.WriteLine("2. 장착 해제");
            Console.WriteLine("0. 돌아가기");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine($"현재 장착된 아이템: {player.EquippedItem?.Name ?? "없음"}");
                    break;
                case 2:
                    player.EquippedItem = null;
                    Console.WriteLine("아이템 장착을 해제했습니다.");
                    break;
                case 0:
                    Console.WriteLine("장착 관리 메뉴에서 나갑니다.");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
    }
}

