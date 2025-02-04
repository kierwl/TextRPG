class ExplorationManager
{
    private Game game;

    public ExplorationManager(Game game)
    {
        this.game = game;
    }

    public void Explore()
    {
        Console.WriteLine("Ž���� ������ �����ϼ���:");
        game.Map.ShowLocations();
        Console.Write("����: ");
        string choice = Console.ReadLine();

        Location location = game.Map.GetLocation(choice);
        if (location != null)
        {
            Console.WriteLine($"{location.Name} ������ Ž���մϴ�...");
            Console.WriteLine(location.Description);

            switch (location.Name)
            {
                case "����":
                    ExploreVillage(location);
                    break;

                case "��":
                    ExploreForest(location);
                    break;

                default:
                    Console.WriteLine("�� ���������� Ư���� �̺�Ʈ�� �����ϴ�.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("�߸��� ���� �����Դϴ�.");
        }
    }

    private void ExploreVillage(Location location)
    {
        Console.WriteLine("�������� ���ΰ� ��ȭ�ϰų�, ����Ʈ�� ���� �� �ֽ��ϴ�.");

        Console.WriteLine("1. ���ΰ� ��ȭ�ϱ�");
        Console.WriteLine("2. ����Ʈ �����ϱ�");
        Console.WriteLine("3. �޽��ϱ�");
        Console.WriteLine("0. ���� ������");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.WriteLine("���ΰ� ��ȭ�մϴ�.");
                game.Shopping();
                break;
            case "2":
                Console.WriteLine("����Ʈ�� �����մϴ�.");
                //AcceptQuest();
                break;
            case "3":
                Console.WriteLine("�޽��� ���մϴ�.");
                game.Player.Health += 20;
                break;
            case "0":
                Console.WriteLine("������ �����ϴ�.");
                break;
            default:
                Console.WriteLine("�߸��� �Է��Դϴ�.");
                break;
        }
    }

    private void ExploreForest(Location location)
    {
        Console.WriteLine("������ ���� �ο�ų�, ������ ã�� �� �ֽ��ϴ�.");

        if (location.HasEnemy)
        {
            game.Battle();
        }
        else
        {
            Console.WriteLine("�� ������ ���� �����ϴ�.");
        }
    }
}
