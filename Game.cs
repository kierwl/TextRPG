using System;
using System.Collections.Generic;
using Textrpg;

class Game
{
    private Player player;
    private Enemy enemy;
    private StatusWindow statusWindow;
    private List<Quest> quests;
    private Map map;

    public void Start()
    {
        Console.WriteLine("�ؽ�Ʈ RPG�� ���� ���� ȯ���մϴ�!");
        Console.Write("�÷��̾� �̸��� �Է��ϼ���: ");
        string name = Console.ReadLine();

        player = new Player(name);
        statusWindow = new StatusWindow();
        map = new Map();
        quests = new List<Quest>
        {
            new Quest("��� ���", "��� 3������ óġ�϶�", 3, "���"),
            new Quest("�� Ž��", "�� ������ Ž���϶�", 1, "��")
        };
        
        
        MainMenu();
    }

    private void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ���� �޴� ===");
            Console.WriteLine("1. ���� Ȯ��");
            Console.WriteLine("2. ���� ����");
            Console.WriteLine("3. ������ ���");
            Console.WriteLine("4. Ž��");
            Console.WriteLine("5. ����Ʈ Ȯ��");
            Console.WriteLine("6. ����");
            Console.Write("����: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    statusWindow.Show(player);
                    break;
                case "2":
                    Battle();
                    break;
                case "3":
                    //player.UseItem();
                    break;
                case "4":
                    Explore();
                    break;
                case "5":
                    ShowQuests();
                    break;
                case "6":
                    Console.WriteLine("������ �����մϴ�.");
                    return;
                default:
                    Console.WriteLine("�߸��� �Է��Դϴ�.");
                    break;
            }
        }
    }

    void Battle()
    {
        Enemy enemy = new Enemy("���", 20, 5, 10);
        BattleManager.StartBattle(player, enemy, quests);
    }

    private void Explore()
    {
        Console.WriteLine("Ž���� ������ �����ϼ���:");
        map.ShowLocations();
        Console.Write("����: ");
        string choice = Console.ReadLine();

        Location location = map.GetLocation(choice);
        if (location != null)
        {
            Console.WriteLine($"{location.Name} ������ Ž���մϴ�...");
            Console.WriteLine(location.Description);

            if (location.HasEnemy)
            {
                Battle();
            }
        }
        else
        {
            Console.WriteLine("�߸��� ���� �����Դϴ�.");
        }
    }

    private void ShowQuests()
    {
        Console.WriteLine("=== ����Ʈ ��� ===");
        foreach (var quest in quests)
        {
            Console.WriteLine($"{quest.Title}: {quest.Description} (���൵: {quest.Progress}/{quest.Goal})");
        }
        Console.ReadLine();
    }
}
