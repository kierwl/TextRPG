class MenuManager
{
    private Game game;

    public MenuManager(Game game)
    {
        this.game = game;
    }

    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== 메인 메뉴 ===");
            Console.WriteLine("1. 상태 확인");
            Console.WriteLine("2. 게임 시작");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 탐험");
            Console.WriteLine("5. 퀘스트 확인");
            Console.WriteLine("6. 종료");
            Console.Write("선택: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    game.StatusWindow.Show(game.Player);
                    break;
                case "2":
                    game.Story();
                    break;
                case "3":
                    game.ShowInventory(game.GetPlayer());
                    break;
                case "4":
                    game.Explore();
                    break;
                case "5":
                    game.ShowQuests();
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
}
