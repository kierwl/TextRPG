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
        Console.WriteLine("텍스트 RPG에 오신 것을 환영합니다!");
        Console.Write("플레이어 이름을 입력하세요: ");
        string name = Console.ReadLine();

        player = new Player(name);
        statusWindow = new StatusWindow();
        map = new Map();
        quests = new List<Quest>
        {
            new Quest("고블린 사냥", "고블린 3마리를 처치하라", 3, "고블린"),
            new Quest("숲 탐험", "숲 지역을 탐험하라", 1, "숲")
        };
        
        
        MainMenu();
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
                    //player.UseItem();
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
        Enemy enemy = new Enemy("고블린", 20, 5, 10);
        BattleManager.StartBattle(player, enemy, quests);
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
