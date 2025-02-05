using Textrpg;

class Game
{
    public Player Player { get; private set; }
    public StatusWindow StatusWindow { get; private set; }
    public List<Quest> Quests { get; private set; }
    public Map Map { get; private set; }
    public Shop Shop { get; private set; }
    public Inventory Inventory { get; private set; }
    public Location location { get; private set; }

    private MenuManager menuManager;
    private ExplorationManager explorationManager;

    public void Start()
    {
        Console.WriteLine("텍스트 RPG에 오신 것을 환영합니다!");
        Console.Write("플레이어 이름을 입력하세요: ");
        string name = Console.ReadLine();

        Job job = SelectJob();  // 직업 선택
        Player = new Player(name, job);  // 직업에 맞게 플레이어 생성
        StatusWindow = new StatusWindow();
        //생성된 Player 객체를 Inventory에 전달
        Inventory = new Inventory(Player);

        Player.Inventory.AddItem(new Item("체력 포션", "체력을 50 회복하는 포션", ItemType.Potion, 0, 0, 0, 50, 0)); // 기본 지급 아이템
        Player.Inventory.AddItem(new Item("철검", "기본적인 철검", ItemType.Weapon, 10, 0, 0, 0, 0));

        Map = new Map();
        Quests = new List<Quest>
        {
            new Quest("고블린 사냥", "고블린 3마리를 처치하라", 3, "고블린", 100),
            new Quest("숲 탐험", "숲 지역을 탐험하라", 1, "숲", 100)
        };
        // Shop 객체 초기화
        Shop = new Shop(Player);

        menuManager = new MenuManager(this);
        explorationManager = new ExplorationManager(this);

        menuManager.MainMenu();
    }

    private Job SelectJob()
    {
        Console.WriteLine("직업을 선택하세요:");
        Console.WriteLine("1. 전사 (Health +20, Attack +5, Defense +3)");
        Console.WriteLine("2. 마법사 (Health +10, Attack +10, Defense +1)");
        Console.WriteLine("3. 궁수 (Health +15, Attack +7, Defense +2)");
        Console.Write("선택: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": return new Job(JobType.Warrior);
            case "2": return new Job(JobType.Mage);
            case "3": return new Job(JobType.Archer);
            case "4": return new Job(JobType.Sparta);
            default:
                Console.WriteLine("잘못된 선택입니다. 기본 직업으로 설정됩니다.");
                return new Job(JobType.무직백수);  // 기본 직업으로 **** 선택
                Console.ReadLine();
        }
    }

    public void Story()
    {
        Console.WriteLine("게임 스토리를 진행합니다.");
        Console.WriteLine("아직 준비중인 기능입니다.");
        Console.ReadLine();
    }

    public void Battle(Enemy enemy)
    {
        
        BattleManager.StartBattle(Player, enemy, Quests, StatusWindow,explorationManager,location);
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
        Console.WriteLine("=== 퀘스트 목록 ===");
        foreach (var quest in Quests)
        {
            Console.WriteLine($"{quest.Title}: {quest.Description} (진행도: {quest.Progress}/{quest.Goal})");
        }
        Console.ReadLine();
    }
    public void Explore()
    {
        explorationManager.Explore();
    }
}
