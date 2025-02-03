using System;
using Textrpg;





class StatusWindow
{
    public void Show(Player player)
    {
        Console.Clear();
        Console.WriteLine("=== 상태창 ===");
        Console.WriteLine($"이름: {player.Name}");
        Console.WriteLine($"직업: {player.job}");
        Console.WriteLine($"레벨: {player.Level}");
        Console.WriteLine($"체력: {player.Health}");
        Console.WriteLine($"공격력: {player.AttackPower}");
        Console.WriteLine($"방어력: {player.Defense}");
        Console.WriteLine($"경험치: {player.Experience}/{player.ExpToNextLevel}");
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
}
