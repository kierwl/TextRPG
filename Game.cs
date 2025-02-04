using Textrpg;

class Game
{
    public Player Player { get; private set; }
    public StatusWindow StatusWindow { get; private set; }
    public List<Quest> Quests { get; private set; }
    public Map Map { get; private set; }
    public Shop Shop { get; private set; }
    public Inventory Inventory { get; private set; }

    private MenuManager menuManager;
    private ExplorationManager explorationManager;

    public void Start()
    {
        Console.WriteLine("�ؽ�Ʈ RPG�� ���� ���� ȯ���մϴ�!");
        Console.Write("�÷��̾� �̸��� �Է��ϼ���: ");
        string name = Console.ReadLine();

        Job job = SelectJob();  // ���� ����
        Player = new Player(name, job);  // ������ �°� �÷��̾� ����
        StatusWindow = new StatusWindow();
        //������ Player ��ü�� Inventory�� ����
        Inventory = new Inventory(Player);

        Player.Inventory.AddItem(new Item("ü�� ����", "ü���� 50 ȸ���ϴ� ����", ItemType.Potion, 0, 0, 0, 50, 0));
        Player.Inventory.AddItem(new Item("ö��", "�⺻���� ö��", ItemType.Weapon, 10, 0, 0, 0, 0));

        Map = new Map();
        Quests = new List<Quest>
        {
            new Quest("��� ���", "��� 3������ óġ�϶�", 3, "���"),
            new Quest("�� Ž��", "�� ������ Ž���϶�", 1, "��")
        };
        // Shop ��ü �ʱ�ȭ
        Shop = new Shop(Player);

        menuManager = new MenuManager(this);
        explorationManager = new ExplorationManager(this);

        menuManager.MainMenu();
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

    public void Story()
    {
        Console.WriteLine("���� ���丮�� �����մϴ�.");
        Console.WriteLine("���� �غ����� ����Դϴ�.");
        Console.ReadLine();
    }

    public void Battle()
    {
        Enemy enemy = new Enemy("���", 20, 5, 10, 1);
        BattleManager.StartBattle(Player, enemy, Quests, StatusWindow);
    }

    public Player GetPlayer()
    {
        return Player;
    }

    public void ShowInventory(Player player)
    {
        player.Inventory.ShowItems();
    }

    public void Shopping()
    {
        Shop.ShowShopItems();
    }

    public void ShowQuests()
    {
        Console.WriteLine("=== ����Ʈ ��� ===");
        foreach (var quest in Quests)
        {
            Console.WriteLine($"{quest.Title}: {quest.Description} (���൵: {quest.Progress}/{quest.Goal})");
        }
        Console.ReadLine();
    }
    public void Explore()
    {
        explorationManager.Explore();
    }
}
