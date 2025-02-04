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
                    Console.WriteLine("������ �����մϴ�.");
                    return;
                default:
                    Console.WriteLine("�߸��� �Է��Դϴ�.");
                    break;
            }
        }
    }
}
