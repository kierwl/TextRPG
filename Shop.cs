using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textrpg
{
    internal class Shop
    {

        private List<Item> ShopItem = new List<Item>(); // 판매대 아이템 목록
        private Player player; // 플레이어 객체

        public Shop(Player player)
        {

            this.player = player;
            ShopItem = new List<Item>
                    {
                        new Item("강철 검", "강철로 만들어진 검", ItemType.Weapon, 10, 0, 0, 0, 400),
                        new Item("강철 갑옷", "강철로 만들어진 갑옷", ItemType.Armor, 0, 10, 0, 0, 500),
                        new Item("체력 물약", "최대 체력을 50 증가시켜주는 물약", ItemType.Potion, 0, 0, 0, 50, 5000),
                        new Item("가시 갑옷", "철갑에 가시를 두른 갑옷이다..", ItemType.Armor, 2, 15, 0, 50, 1000),
                        new Item("낡은 놋쇠 단검", "부식되어 쓸만한 상태는 아니다.", ItemType.Weapon, 3, 0, 0, 50, 100)
                    };
        }

        public void ShowShopItems()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("+++++++++++++++++++++++++++++++");
                Console.WriteLine("상점에 오신 것을 환영합니다!");
                Console.WriteLine($"[보유 골드] {player.Gold} G\n");
                Console.WriteLine("+++++++++++++++++++++++++++++++");
                Console.WriteLine("[아이템 목록]");


                foreach (var item in ShopItem)
                {
                    string status = item.IsPurchased ? "구매완료" : $"{item.Price} G";
                    Console.WriteLine($"- {item.Name} | {item.Description} | {status}");
                }
                Console.WriteLine("+++++++++++++++++++++++++++++++");
                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");
                Console.Write("원하시는 행동을 입력해주세요: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        BuyItem();
                        break;

                    case 2:
                        SellItem();
                        break;
                    case 0:
                        Console.WriteLine("상점을 떠납니다.");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }

        public void BuyItem()
        {
            Console.WriteLine("구매할 아이템 번호를 입력하세요.");

            for (int i = 0; i < ShopItem.Count; i++)
            {
                var item = ShopItem[i];
                string status = item.IsPurchased ? "구매완료" : $"{item.Price} G";
                Console.WriteLine($"{i + 1}. {item.Name} | {item.Description} | {status}");
            }

            Console.Write("선택: ");
            if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > ShopItem.Count)
            {
                Console.WriteLine("잘못된 선택입니다.");
                return;
            }

            itemIndex--; // 리스트 인덱스 보정
            var selectedItem = ShopItem[itemIndex];

            if (selectedItem.IsPurchased)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                return;
            }

            if (player.Gold >= selectedItem.Price)
            {
                // 골드 차감 및 상태 변경
                player.Gold -= selectedItem.Price;
                selectedItem.IsPurchased = true;

                // **원본 아이템의 모든 정보를 그대로 복사하여 인벤토리에 추가**
                player.Inventory.AddItem(new Item(
                    selectedItem.Name,
                    selectedItem.Description,
                    selectedItem.Type,
                    selectedItem.AttackValue,
                    selectedItem.DefenseValue,
                    selectedItem.SpeedValue,
                    selectedItem.HealthValue,
                    selectedItem.Price
                ));

                Console.WriteLine($"{selectedItem.Name}을(를) 구매하여 인벤토리에 추가했습니다! 현재 보유 골드: {player.Gold} G");
            }
            else
            {
                Console.WriteLine("골드가 부족합니다.");
            }


        }

        public void SellItem()
        {
            if (player.Inventory.GetItems().Count == 0)
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
                return;
            }

            Console.WriteLine("판매할 아이템 번호를 입력하세요.");

            for (int i = 0; i < player.Inventory.GetItems().Count; i++)
            {
                Item item = player.Inventory.GetItems()[i];
                int itemSellPrice = (int)(item.Price / 1.15); // 판매 가격 = 원래 가격의 80%?
                Console.WriteLine($"{i + 1}. {item.Name} | {item.Description} | 판매 가격: {itemSellPrice} G");
            }

            Console.Write("\n선택: ");
            if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > player.Inventory.GetItems().Count)
            {
                Console.WriteLine("잘못된 선택입니다.");
                return;
            }

            itemIndex--; // 리스트 인덱스 보정
            Item selectedItem = player.Inventory.GetItems()[itemIndex];
            int sellPrice = (int)(selectedItem.Price / 1.15);

            // 아이템 판매
            player.Inventory.RemoveItem(selectedItem);
            player.Gold += sellPrice;

            Console.WriteLine($"{selectedItem.Name}을(를) {sellPrice} G에 판매했습니다! 현재 보유 골드: {player.Gold} G");
        }





    }
}
