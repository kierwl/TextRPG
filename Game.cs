using Textrpg;

class Game
{
    private Player player;
    private StatusWindow statusWindow;
    private List<Quest> quests;
    private Map map;

    public void Start()
    {
        Console.WriteLine("텍스트 RPG에 오신 것을 환영합니다!");
        Console.Write("플레이어 이름을 입력하세요: ");
        string name = Console.ReadLine();

        Job job = SelectJob();  // 직업 선택
        player = new Player(name, job);  // 직업에 맞게 플레이어 생성
        statusWindow = new StatusWindow();

        player.Inventory.AddItem(new Item("체력 포션", "체력을 50 회복하는 포션", ItemType.Potion, 50));
        player.Inventory.AddItem(new Item("철검", "기본적인 철검", ItemType.Weapon, 10));

        map = new Map();
        quests = new List<Quest>
        {
            new Quest("고블린 사냥", "고블린 3마리를 처치하라", 3, "고블린"),
            new Quest("숲 탐험", "숲 지역을 탐험하라", 1, "숲")
        };


        MainMenu();
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
            default:
                Console.WriteLine("잘못된 선택입니다. 기본 직업(전사)으로 설정됩니다.");
                return new Job(JobType.Warrior);  // 기본 직업으로 전사 선택
        }
    }
    private void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== 메인 메뉴 ===");
            Console.WriteLine("1. 상태 확인");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 아이템 사용");
            Console.WriteLine("4. 탐험");
            Console.WriteLine("5. 퀘스트 확인");
            Console.WriteLine("6. 종료");
            Console.Write("선택: ");

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
                    ShowInventory();
                    break;
                case "4":
                    Explore();
                    break;
                case "5":
                    ShowQuests();
                    break;
                case "6":
                    Console.WriteLine("게임을 종료합니다.");
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
    }

    void Battle()
    {
        Enemy enemy = new Enemy("고블린", 20, 5, 10,1);
        BattleManager.StartBattle(player, enemy, quests);
    }
    public void ShowInventory()
    {
        player.Inventory.ShowItems();  // 인벤토리 출력
        Console.WriteLine("아이템을 사용하려면 번호를 입력하세요. (0으로 돌아가기)");
        int choice = int.Parse(Console.ReadLine()) - 1;

        if (choice >= 0)
        {
            player.Inventory.UseItem(choice, player);  // 아이템 사용
        }
        else
        {
            Console.WriteLine("돌아갑니다.");
        }
    }

    private void Explore()
    {
        Console.WriteLine("탐험할 지역을 선택하세요:");
        map.ShowLocations();
        Console.Write("선택: ");
        string choice = Console.ReadLine();

        Location location = map.GetLocation(choice);
        if (location != null)
        {
            Console.WriteLine($"{location.Name} 지역을 탐험합니다...");
            Console.WriteLine(location.Description);

            if (location.HasEnemy)
            {
                Battle();
            }
        }
        else
        {
            Console.WriteLine("잘못된 지역 선택입니다.");
        }
    }

    private void ShowQuests()
    {
        Console.WriteLine("=== 퀘스트 목록 ===");
        foreach (var quest in quests)
        {
            Console.WriteLine($"{quest.Title}: {quest.Description} (진행도: {quest.Progress}/{quest.Goal})");
        }
        Console.ReadLine();
    }
}
