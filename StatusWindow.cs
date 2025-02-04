using System;
using System.Numerics;
using Textrpg;





class StatusWindow
{
    public void Show(Player player)
    {
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
        Console.WriteLine("=== ����â ===");
        Console.WriteLine($"�̸�: {player.Name}");
        Console.WriteLine($"����: {player.job.Name}");
        Console.WriteLine($"����: {player.Level}");
        Console.WriteLine($"Gold : {player.Gold}");

        Console.WriteLine($"ü��: {displayhp}(+{displayhp - player.Health})");
        Console.WriteLine($"���ݷ�: {displayatk}(+{displayatk - player.AttackPower})");
        Console.WriteLine($"����: {displaydef}(+{displaydef - player.Defense})");
        Console.WriteLine($"����ġ: {player.Experience}/{player.ExpToNextLevel}");
        Console.WriteLine("-----------------");
        Console.WriteLine("���� ������:");

        // ������ ������ ���
        if (player.Inventory.GetEquippedItems().Count == 0)
        {
            Console.WriteLine("������ �������� �����ϴ�.");
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
