using Textrpg;

class Game
{
    private Player player;
    private StatusWindow statusWindow;
    private List<Quest> quests;
    private Map map;
    private Inventory inventory;
    public void Start()
    {
        Console.WriteLine("�ؽ�Ʈ RPG�� ���� ���� ȯ���մϴ�!");
        Console.Write("�÷��̾� �̸��� �Է��ϼ���: ");
        string name = Console.ReadLine();

        Job job = SelectJob();  // ���� ����
        player = new Player(name, job);  // ������ �°� �÷��̾� ����
        statusWindow = new StatusWindow();
        //������ Player ��ü�� Inventory�� ����
        inventory = new Inventory(player);

        player.Inventory.AddItem(new Item("ü�� ����", "ü���� 50 ȸ���ϴ� ����", ItemType.Potion, 0,0,0,50));
        player.Inventory.AddItem(new Item("ö��", "�⺻���� ö��", ItemType.Weapon, 10,0,0,0));

        map = new Map();
        quests = new List<Quest>
        {
            new Quest("��� ���", "��� 3������ óġ�϶�", 3, "���"),
            new Quest("�� Ž��", "�� ������ Ž���϶�", 1, "��")
        };


        MainMenu();
    }
    private Job SelectJob()
    {
        Console.WriteLine("������ �����ϼ���:");
        Console.WriteLine("1. ���� (Health +20, Attack +5, Defense +3)");
        Console.WriteLine("2. ������ (Health +10, Attack +10, Defense +1)");
        Console.WriteLine("3. �ü� (Health +15, Attack +7, Defense +2)");
        Console.Write("����: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": return new Job(JobType.Warrior);
            case "2": return new Job(JobType.Mage);
            case "3": return new Job(JobType.Archer);
            default:
                Console.WriteLine("�߸��� �����Դϴ�. �⺻ ����(����)���� �����˴ϴ�.");
                return new Job(JobType.Warrior);  // �⺻ �������� ���� ����
        }
    }
    private void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ���� �޴� ===");
            Console.WriteLine("1. ���� Ȯ��");
            Console.WriteLine("2. ���� ����");
            Console.WriteLine("3. �κ��丮");
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
                    ShowInventory(GetPlayer());
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
        Enemy enemy = new Enemy("���", 20, 5, 10,1);
        BattleManager.StartBattle(player, enemy, quests);
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void ShowInventory(Player player)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("[������ ���]");
            player.Inventory.ShowItems();  // �÷��̾��� �κ��丮 ������ ���
            Console.WriteLine("\n1. ���� ����");
            Console.WriteLine("0. ������");

            Console.Write("���Ͻô� �ൿ�� �Է����ּ���: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    player.Inventory.EquipManager(player);  // ���� ����
                    break;
                case 0:
                    Console.WriteLine("�κ��丮 �޴��� �����մϴ�.");
                    return;  // �κ��丮 ����
                default:
                    Console.WriteLine("�߸��� �Է��Դϴ�.");
                    break;
            }
            Console.WriteLine("����Ϸ��� ���͸� ��������...");
            Console.ReadLine();
        }
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
