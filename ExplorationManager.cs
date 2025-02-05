using System.Security.Cryptography.X509Certificates;
using Textrpg;

class ExplorationManager
{
    private Game game;
    public static Random random = new Random();
    private Player player;
    public ExplorationManager(Game game)
    {
        this.game = game;
        this.player = game.Player; // player �ʱ�ȭ
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
                case "����":
                    EnterDungeon(location);
                    break;
                case "�縷":
                    ExploreDesert(location);
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
                AcceptQuest();  
                break;
            case "3":
                Console.WriteLine("�޽��� ���մϴ�.");
                Rest();
                break;
            case "0":
                Console.WriteLine("������ �����ϴ�.");
                break;
            default:
                Console.WriteLine("�߸��� �Է��Դϴ�.");
                break;
        }
    }
    public void Rest()
    {
        Console.WriteLine($"500G�� ���� ü���� ȸ�� �� �� �ֽ��ϴ�. �������{player.Gold}");

        Console.WriteLine("1. 500G �����ϱ�");

        Console.WriteLine("2. ������");

        Console.WriteLine("���� :");

        string choice = Console.ReadLine();
        if (choice == "2")
        {
            return;
        }
        else
        {
            if (game.Player.Health == game.Player.MaxHP)
            {
                Console.WriteLine("ü���� �̹� ���� ���ֽ��ϴ�.");
                Console.ReadLine();
                return;
            }

            else if (game.Player.Gold < 500)
            {
                Console.WriteLine("��尡 �����մϴ�.");
                Console.ReadLine();
                return;
            }
            else
            {
                game.Player.Health = game.Player.MaxHP;
                Console.WriteLine("ü���� ȸ���Ǿ����ϴ�.");
                game.Player.Gold -= 500;
                Console.ReadLine();
            }


        }
    }

    public void ExploreForest(Location location)
    {
        if (location == null)
        {
            Console.WriteLine("���� Ž���մϴ�.");
            location = game.Map.GetLocation("��");
        }

        int encounter = random.Next(1, 101);
        if (encounter <= 30)
        {
            location.HasEnemy = true;
        }
        else if (encounter < 10)
        {
            Console.WriteLine("����� ���� �Ѹ� �ؿ��� ��¦�̴� ��ȭ�� �߰��߽��ϴ�!");
            int goldFound = random.Next(100, 501); // 100~500G ���� ȹ��
            Console.WriteLine($"{goldFound}G�� ȹ���Ͽ����ϴ�.");
            game.Player.Gold += goldFound;

            BattleManager.NextStage(player, this, location);
        }
        else
        {
            //location.HasEnemy = false;
        }

        Console.WriteLine("������ ���� �߰� �� �� �ֽ��ϴ�.");

        if (location.HasEnemy)
        {
            Enemy enemy = MonsterList.GetRandomMonster();
            game.Battle(enemy);
        }
        else
        {
            Console.WriteLine("�� ������ ���� �����ϴ�.");
        }
    }
    public void ExploreDesert(Location location) // �縷  Ž��
    {
        Console.WriteLine("�縷�� Ž���մϴ�. �߰ſ� �¾��� �Ӹ� ������ �ۿ��մϴ�...");
        if (location == null)
        {
            Console.WriteLine("�縷�� Ž���մϴ�");
            location = game.Map.GetLocation("�縷");
        }

        int encounter = random.Next(1, 101);

        if (encounter <= 25)
        {
            Console.WriteLine("����� �縷���� ���� �Ҿ����ϴ�. ü���� 10 �����մϴ�.");
            game.Player.Health -= 10;
            BattleManager.NextStage(game.Player, this, location);

        }
        else if (encounter <= 40)
        {
            Console.WriteLine("�� �ӿ��� ���� ������ �߰��߽��ϴ�!");
            int goldFound = random.Next(10, 1001); // 10~1000G ���� ȹ��
            Console.WriteLine($"{goldFound}G�� ȹ���Ͽ����ϴ�.");
            game.Player.Gold += goldFound;
            BattleManager.NextStage(game.Player, this, location);

        }
        else if (encounter <= 75)
        {
            Console.WriteLine("�縷�� ������ ���Ͱ� ��Ÿ�����ϴ�!");
            location.HasEnemy = true;

            
        }
        else
        {
            Console.WriteLine("�߰ſ� �¾� �Ʒ� ���ƽý��� �߰��߽��ϴ�. ü���� 15 ȸ���˴ϴ�.");
            game.Player.Health += 15;
            BattleManager.NextStage(game.Player, this, location);

        }

        if (location.HasEnemy)
        {
            Enemy enemy = MonsterList.GetRandomMonster2();
            game.Battle(enemy);
        }
        else
        {
            Console.WriteLine("�� ������ ���� �����ϴ�.");
            
        }
    }

    private void AcceptQuest() // ����Ʈ ����
    {
        Console.WriteLine("\n[����Ʈ ���]");

        // ���� ������ ����Ʈ ���
        List<Quest> availableQuests = new List<Quest>
    {
        new Quest("�� ���� ����", "�� �ӿ� ��Ÿ�� ������ óġ���ּ���.", 500, "�ں�Ʈ", 100),
        new Quest("�Ҿ���� ����", "���� �ֹ��� �Ҿ���� ������ ã���ּ���.", 300, "����",100),
        new Quest("��ũ �θ� óġ", "���� ��ó ��ũ �θ��� óġ�ϼ���.", 1000, "��ũ", 100),
        new Quest("��� ���� Ž��", "���� ���� ������ ��� ������ �߰��ϼ���.", 1500, "����",100)
    };

        // ���� ������ ����Ʈ ��� (Game Ŭ�������� ����)
        List<Quest> playerQuests = game.Quests;

        // ����Ʈ ��� ���
        for (int i = 0; i < availableQuests.Count; i++)
        {
            Quest quest = availableQuests[i];
            bool isAccepted = playerQuests.Any(q => q.Title == quest.Title);

            // ������ ����Ʈ�� "���� �Ϸ�" ǥ��
            string status = isAccepted ? "[���� �Ϸ�]" : "";
            Console.WriteLine($"{i + 1}. {quest.Title} - ����: {quest.Reward} Gold {status}");
        }

        Console.WriteLine("0. ���");

        Console.Write("\n������ ����Ʈ ��ȣ�� �����ϼ���: ");
        string choice = Console.ReadLine();

        if (int.TryParse(choice, out int questIndex) && questIndex > 0 && questIndex <= availableQuests.Count)
        {
            Quest selectedQuest = availableQuests[questIndex - 1];

            // �ߺ� ����Ʈ üũ
            if (playerQuests.Any(q => q.Title == selectedQuest.Title))
            {
                Console.WriteLine("\n�̹� ������ ����Ʈ�Դϴ�!");
                return;
            }

            // ����Ʈ�� �÷��̾��� ��Ͽ� �߰�
            playerQuests.Add(selectedQuest);
            Console.WriteLine($"\n'{selectedQuest.Title}' ����Ʈ�� �����Ͽ����ϴ�!");
        }
        else if (questIndex == 0)
        {
            Console.WriteLine("����Ʈ ������ ����Ͽ����ϴ�.");
        }
        else
        {
            Console.WriteLine("�߸��� �Է��Դϴ�.");
        }
    }


    private void EnterDungeon(Location location) // ���� ���� ����
    {
        Console.WriteLine("������ �����մϴ�.");
        Console.WriteLine("������ ��ȣ�ڰ� ����� �ֽ��մϴ�.");
        if (game.Player.Level < 5)
        {
            Console.WriteLine("������ ��ȣ��: ���� ������ �ʹ� ����. ������.");
            Console.WriteLine("������ ���� �ڷ���Ʈ �˴ϴ�.!!");
            Console.ReadLine();
            ExploreForest(location);
        }
        else
        {
            Console.WriteLine("������ ��ȣ��: ���� �ɷ��� �����غ��ڴ�.");
            SelectDungeon();
        }
    }
    private void SelectDungeon() // ���� ����
    {
        Console.WriteLine("1. ���� ���� (���� 15 �̻� ����)");
        Console.WriteLine("2. �Ϲ� ���� (���� 29 �̻� ����)");
        Console.WriteLine("3. ����� ���� (���� 57 �̻� ����)");
        Console.WriteLine("0. ������");

        Console.Write("���Ͻô� �ൿ�� �Է����ּ���: ");
        string choice = Console.ReadLine();
        int requiredDefense = 0, baseReward = 0;

        switch (choice)
        {
            case "1":
                requiredDefense = 15;
                baseReward = 1000;
                Console.WriteLine("���� ������ �����մϴ�...");
                break;
            case "2":
                requiredDefense = 29;
                baseReward = 1700;
                Console.WriteLine("�Ϲ� ������ �����մϴ�...");
                break;
            case "3":
                requiredDefense = 57;
                baseReward = 2500;
                Console.WriteLine("����� ������ �����մϴ�...");
                break;
            case "0":
                Console.WriteLine("������ �����ϴ�.");
                return;
            default:
                Console.WriteLine("�߸��� �Է��Դϴ�.");
                return;
        }
        ProcessDungeon(requiredDefense, baseReward);




    }

        


        
    private void ProcessDungeon(int requiredDefense, int baseReward)//���� Ŭ���� ���� �� ���� ó��
    {
        Player player = game.Player;
        int defenseGap = player.Defense - requiredDefense;
        int failChance = 40;
        bool isFailed = (player.Defense < requiredDefense) && (random.Next(100) < failChance);

        if (isFailed)
        {
            Console.WriteLine("\n���� ������ �����߽��ϴ�!");
            int healthLoss = player.Health / 2;
            player.Health -= healthLoss;
            Console.WriteLine($"ü���� ���� �����Ͽ����ϴ�! ���� ü��: {player.MaxHP}/{player.MaxHP}");
            Console.ReadLine();
            return;
        }

        // ���� �� ü�� ���� ���
        int minHealthLoss = 20 + Math.Max(0, defenseGap);
        int maxHealthLoss = 35 + Math.Max(0, defenseGap);
        int healthLossFinal = random.Next(minHealthLoss, maxHealthLoss + 1);
        player.Health -= healthLossFinal;

        // ���� ���
        int attackBonus = (int)(baseReward * (random.Next(player.AttackPower, player.AttackPower*2 + 1) / 100.0));
        int totalReward = baseReward + attackBonus;
        player.Gold += totalReward;

        Console.WriteLine("\n ���� Ŭ����!");
        Console.WriteLine($"[Ž�� ���]\nü�� {player.MaxHP + healthLossFinal} -> {player.MaxHP}");
        Console.WriteLine($"Gold {player.Gold - totalReward} -> {player.Gold}");

        Console.WriteLine("\n0. ������");
        Console.Write("���Ͻô� �ൿ�� �Է����ּ���: ");
        string exitChoice = Console.ReadLine();
        if (exitChoice == "0")
            Console.WriteLine("������ �����ϴ�.");
    }
}
