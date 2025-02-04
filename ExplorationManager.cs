class ExplorationManager
{
    private Game game;

    public ExplorationManager(Game game)
    {
        this.game = game;
    }

    public void Explore()
    {
        Console.WriteLine("탐험할 지역을 선택하세요:");
        game.Map.ShowLocations();
        Console.Write("선택: ");
        string choice = Console.ReadLine();

        Location location = game.Map.GetLocation(choice);
        if (location != null)
        {
            Console.WriteLine($"{location.Name} 지역을 탐험합니다...");
            Console.WriteLine(location.Description);

            switch (location.Name)
            {
                case "마을":
                    ExploreVillage(location);
                    break;

                case "숲":
                    ExploreForest(location);
                    break;

                default:
                    Console.WriteLine("이 지역에서는 특별한 이벤트가 없습니다.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("잘못된 지역 선택입니다.");
        }
    }

    private void ExploreVillage(Location location)
    {
        Console.WriteLine("마을에서 상인과 대화하거나, 퀘스트를 받을 수 있습니다.");

        Console.WriteLine("1. 상인과 대화하기");
        Console.WriteLine("2. 퀘스트 수주하기");
        Console.WriteLine("3. 휴식하기");
        Console.WriteLine("0. 마을 나가기");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.WriteLine("상인과 대화합니다.");
                game.Shopping();
                break;
            case "2":
                Console.WriteLine("퀘스트를 수주합니다.");
                //AcceptQuest();
                break;
            case "3":
                Console.WriteLine("휴식을 취합니다.");
                game.Player.Health += 20;
                break;
            case "0":
                Console.WriteLine("마을을 나갑니다.");
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                break;
        }
    }

    private void ExploreForest(Location location)
    {
        Console.WriteLine("숲에서 적과 싸우거나, 보물을 찾을 수 있습니다.");

        if (location.HasEnemy)
        {
            game.Battle();
        }
        else
        {
            Console.WriteLine("이 숲에는 적이 없습니다.");
        }
    }
}
