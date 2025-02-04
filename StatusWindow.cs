using System;
using System.Numerics;
using Textrpg;





class StatusWindow
{
    public void Show(Player player)
    {
        var totalStats = GetTotalStats(player);
        int displayatk = player.AttackPower;
        int displaydef = player.Defense;
        int displayhp = player.Health;
        foreach (var item in player.Inventory.GetEquippedItems())
        {
            displayatk += item.AttackValue;
            displaydef += item.DefenseValue;
            displayhp += item.HealthValue;
        }

        Console.Clear();
        Console.WriteLine("=== 상태창 ===");
        Console.WriteLine($"이름: {player.Name}");
        Console.WriteLine($"직업: {player.job.Name}");
        Console.WriteLine($"레벨: {player.Level}");
        Console.WriteLine($"Gold : {player.Gold}");

        Console.WriteLine($"체력: {player.MaxHP}(+{displayhp - player.Health})");
        Console.WriteLine($"공격력: {displayatk}(+{displayatk - player.AttackPower})");
        Console.WriteLine($"방어력: {displaydef}(+{displaydef - player.Defense})");
        Console.WriteLine($"경험치: {player.Experience}/{player.ExpToNextLevel}");
        Console.WriteLine("-----------------");
        Console.WriteLine("장착 아이템:");

        // 장착된 아이템 출력
        if (player.Inventory.GetEquippedItems().Count == 0)
        {
            Console.WriteLine("장착된 아이템이 없습니다.");
        }
        else
        {
            foreach (var item in player.Inventory.GetEquippedItems())
            {
                Console.WriteLine($"- [E] {item.Name} | {item.Description}");
            }
        }

        Console.WriteLine("-----------------");

        Console.WriteLine("=================");
        Console.WriteLine("0. 나가기");
        Console.WriteLine("=================");
        Console.Write("원하시는 행동을 입력해주세요. >> ");

        string input = Console.ReadLine();
        if (input == "0")
        {
            return; // 상태 보기 종료
        }
    }

    public (int AttackPower, int Defense, int Health) GetTotalStats(Player player)
    {
        int totalAtk = player.AttackPower;
        int totalDef = player.Defense;
        int totalHp = player.Health;

        foreach (var item in player.Inventory.GetEquippedItems())
        {
            totalAtk += item.AttackValue;
            totalDef += item.DefenseValue;
            totalHp += item.HealthValue;
        }

        return (totalAtk, totalDef, totalHp);
    }
}

    

