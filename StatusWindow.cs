using System;

class StatusWindow
{
    public void Show(Player player)
    {
        Console.Clear();
        Console.WriteLine("=== ����â ===");
        Console.WriteLine($"�̸�: {player.Name}");
        Console.WriteLine($"����: {player.Level}");
        Console.WriteLine($"ü��: {player.Health}");
        Console.WriteLine($"���ݷ�: {player.AttackPower}");
        Console.WriteLine($"����ġ: {player.Experience}/{player.ExpToNextLevel}");
        Console.WriteLine("=================");
        Console.ReadLine();
    }
}
