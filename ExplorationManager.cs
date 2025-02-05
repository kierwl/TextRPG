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
        this.player = game.Player; // player 초기화
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
                case "던전":
                    EnterDungeon(location);
                    break;
                case "사막":
                    ExploreDesert(location);
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
                AcceptQuest();  
                break;
            case "3":
                Console.WriteLine("휴식을 취합니다.");
                Rest();
                break;
            case "0":
                Console.WriteLine("마을을 나갑니다.");
                break;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                break;
        }
    }
    public void Rest()
    {
        Console.WriteLine($"500G를 내면 체력을 회복 할 수 있습니다. 보유골드{player.Gold}");

        Console.WriteLine("1. 500G 지불하기");

        Console.WriteLine("2. 나가기");

        Console.WriteLine("선택 :");

        string choice = Console.ReadLine();
        if (choice == "2")
        {
            return;
        }
        else
        {
            if (game.Player.Health == game.Player.MaxHP)
            {
                Console.WriteLine("체력이 이미 가득 차있습니다.");
                Console.ReadLine();
                return;
            }

            else if (game.Player.Gold < 500)
            {
                Console.WriteLine("골드가 부족합니다.");
                Console.ReadLine();
                return;
            }
            else
            {
                game.Player.Health = game.Player.MaxHP;
                Console.WriteLine("체력이 회복되었습니다.");
                game.Player.Gold -= 500;
                Console.ReadLine();
            }


        }
    }

    public void ExploreForest(Location location)
    {
        if (location == null)
        {
            Console.WriteLine("숲을 탐험합니다.");
            location = game.Map.GetLocation("숲");
        }

        int encounter = random.Next(1, 101);
        if (encounter <= 30)
        {
            location.HasEnemy = true;
        }
        else if (encounter < 10)
        {
            Console.WriteLine("당신은 나무 뿌리 밑에서 반짝이는 금화를 발견했습니다!");
            int goldFound = random.Next(100, 501); // 100~500G 랜덤 획득
            Console.WriteLine($"{goldFound}G를 획득하였습니다.");
            game.Player.Gold += goldFound;

            BattleManager.NextStage(player, this, location);
        }
        else
        {
            //location.HasEnemy = false;
        }

        Console.WriteLine("숲에서 무언가 발견 할 수 있습니다.");

        if (location.HasEnemy)
        {
            Enemy enemy = MonsterList.GetRandomMonster();
            game.Battle(enemy);
        }
        else
        {
            Console.WriteLine("이 숲에는 적이 없습니다.");
        }
    }
    public void ExploreDesert(Location location) // 사막  탐험
    {
        Console.WriteLine("사막을 탐험합니다. 뜨거운 태양이 머리 위에서 작열합니다...");
        if (location == null)
        {
            Console.WriteLine("사막을 탐험합니다");
            location = game.Map.GetLocation("사막");
        }

        int encounter = random.Next(1, 101);

        if (encounter <= 25)
        {
            Console.WriteLine("당신은 사막에서 길을 잃었습니다. 체력이 10 감소합니다.");
            game.Player.Health -= 10;
            BattleManager.NextStage(game.Player, this, location);

        }
        else if (encounter <= 40)
        {
            Console.WriteLine("모래 속에서 작은 보물을 발견했습니다!");
            int goldFound = random.Next(10, 1001); // 10~1000G 랜덤 획득
            Console.WriteLine($"{goldFound}G를 획득하였습니다.");
            game.Player.Gold += goldFound;
            BattleManager.NextStage(game.Player, this, location);

        }
        else if (encounter <= 75)
        {
            Console.WriteLine("사막의 강력한 몬스터가 나타났습니다!");
            location.HasEnemy = true;

            
        }
        else
        {
            Console.WriteLine("뜨거운 태양 아래 오아시스를 발견했습니다. 체력이 15 회복됩니다.");
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
            Console.WriteLine("이 숲에는 적이 없습니다.");
            
        }
    }

    private void AcceptQuest() // 퀘스트 수락
    {
        Console.WriteLine("\n[퀘스트 목록]");

        // 수락 가능한 퀘스트 목록
        List<Quest> availableQuests = new List<Quest>
    {
        new Quest("숲 속의 괴물", "숲 속에 나타난 괴물을 처치해주세요.", 500, "코볼트", 100),
        new Quest("잃어버린 반지", "마을 주민이 잃어버린 반지를 찾아주세요.", 300, "반지",100),
        new Quest("오크 두목 처치", "마을 근처 오크 두목을 처치하세요.", 1000, "오크", 100),
        new Quest("고대 유물 탐색", "던전 깊은 곳에서 고대 유물을 발견하세요.", 1500, "던전",100)
    };

        // 현재 수락한 퀘스트 목록 (Game 클래스에서 관리)
        List<Quest> playerQuests = game.Quests;

        // 퀘스트 목록 출력
        for (int i = 0; i < availableQuests.Count; i++)
        {
            Quest quest = availableQuests[i];
            bool isAccepted = playerQuests.Any(q => q.Title == quest.Title);

            // 수락된 퀘스트는 "수락 완료" 표시
            string status = isAccepted ? "[수락 완료]" : "";
            Console.WriteLine($"{i + 1}. {quest.Title} - 보상: {quest.Reward} Gold {status}");
        }

        Console.WriteLine("0. 취소");

        Console.Write("\n수락할 퀘스트 번호를 선택하세요: ");
        string choice = Console.ReadLine();

        if (int.TryParse(choice, out int questIndex) && questIndex > 0 && questIndex <= availableQuests.Count)
        {
            Quest selectedQuest = availableQuests[questIndex - 1];

            // 중복 퀘스트 체크
            if (playerQuests.Any(q => q.Title == selectedQuest.Title))
            {
                Console.WriteLine("\n이미 수락한 퀘스트입니다!");
                return;
            }

            // 퀘스트를 플레이어의 목록에 추가
            playerQuests.Add(selectedQuest);
            Console.WriteLine($"\n'{selectedQuest.Title}' 퀘스트를 수락하였습니다!");
        }
        else if (questIndex == 0)
        {
            Console.WriteLine("퀘스트 수락을 취소하였습니다.");
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
    }


    private void EnterDungeon(Location location) // 던전 입장 문구
    {
        Console.WriteLine("던전에 진입합니다.");
        Console.WriteLine("던전의 수호자가 당신을 주시합니다.");
        if (game.Player.Level < 5)
        {
            Console.WriteLine("던전의 수호자: 너의 방어력이 너무 낮아. 나가라.");
            Console.WriteLine("숲으로 강제 텔레포트 됩니다.!!");
            Console.ReadLine();
            ExploreForest(location);
        }
        else
        {
            Console.WriteLine("던전의 수호자: 너의 능력을 시험해보겠다.");
            SelectDungeon();
        }
    }
    private void SelectDungeon() // 던전 선택
    {
        Console.WriteLine("1. 쉬운 던전 (방어력 15 이상 권장)");
        Console.WriteLine("2. 일반 던전 (방어력 29 이상 권장)");
        Console.WriteLine("3. 어려운 던전 (방어력 57 이상 권장)");
        Console.WriteLine("0. 나가기");

        Console.Write("원하시는 행동을 입력해주세요: ");
        string choice = Console.ReadLine();
        int requiredDefense = 0, baseReward = 0;

        switch (choice)
        {
            case "1":
                requiredDefense = 15;
                baseReward = 1000;
                Console.WriteLine("쉬운 던전에 입장합니다...");
                break;
            case "2":
                requiredDefense = 29;
                baseReward = 1700;
                Console.WriteLine("일반 던전에 입장합니다...");
                break;
            case "3":
                requiredDefense = 57;
                baseReward = 2500;
                Console.WriteLine("어려운 던전에 입장합니다...");
                break;
            case "0":
                Console.WriteLine("던전을 나갑니다.");
                return;
            default:
                Console.WriteLine("잘못된 입력입니다.");
                return;
        }
        ProcessDungeon(requiredDefense, baseReward);




    }

        


        
    private void ProcessDungeon(int requiredDefense, int baseReward)//던전 클리어 보상 및 실패 처리
    {
        Player player = game.Player;
        int defenseGap = player.Defense - requiredDefense;
        int failChance = 40;
        bool isFailed = (player.Defense < requiredDefense) && (random.Next(100) < failChance);

        if (isFailed)
        {
            Console.WriteLine("\n던전 공략에 실패했습니다!");
            int healthLoss = player.Health / 2;
            player.Health -= healthLoss;
            Console.WriteLine($"체력이 절반 감소하였습니다! 현재 체력: {player.MaxHP}/{player.MaxHP}");
            Console.ReadLine();
            return;
        }

        // 성공 시 체력 감소 계산
        int minHealthLoss = 20 + Math.Max(0, defenseGap);
        int maxHealthLoss = 35 + Math.Max(0, defenseGap);
        int healthLossFinal = random.Next(minHealthLoss, maxHealthLoss + 1);
        player.Health -= healthLossFinal;

        // 보상 계산
        int attackBonus = (int)(baseReward * (random.Next(player.AttackPower, player.AttackPower*2 + 1) / 100.0));
        int totalReward = baseReward + attackBonus;
        player.Gold += totalReward;

        Console.WriteLine("\n 던전 클리어!");
        Console.WriteLine($"[탐험 결과]\n체력 {player.MaxHP + healthLossFinal} -> {player.MaxHP}");
        Console.WriteLine($"Gold {player.Gold - totalReward} -> {player.Gold}");

        Console.WriteLine("\n0. 나가기");
        Console.Write("원하시는 행동을 입력해주세요: ");
        string exitChoice = Console.ReadLine();
        if (exitChoice == "0")
            Console.WriteLine("던전을 나갑니다.");
    }
}
