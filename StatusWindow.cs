using System;
using Textrpg;





class StatusWindow
{
    public void Show(Player player)
    {
        Console.Clear();
        Console.WriteLine("=== ����â ===");
        Console.WriteLine($"�̸�: {player.Name}");
        Console.WriteLine($"����: {player.job}");
        Console.WriteLine($"����: {player.Level}");
        Console.WriteLine($"ü��: {player.Health}");
        Console.WriteLine($"���ݷ�: {player.AttackPower}");
        Console.WriteLine($"����: {player.Defense}");
        Console.WriteLine($"����ġ: {player.Experience}/{player.ExpToNextLevel}");
        Console.WriteLine("=================");
        Console.WriteLine("0. ������");
        Console.WriteLine("=================");
        Console.Write("���Ͻô� �ൿ�� �Է����ּ���. >> ");

        string input = Console.ReadLine();
        if (input == "0")
        {
            return; // ���� ���� ����
        }
    }
}
