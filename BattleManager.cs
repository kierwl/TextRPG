using System;
using System.Collections.Generic;

namespace Textrpg
{
    class BattleManager
    {
        private StatusWindow statusWindow;
        private Player Player;
        private Enemy Enemy;
        private ExplorationManager explorationManager; // explorationManager 필드 추가
        private Location location; // location 필드 추가

        public BattleManager(Player player, Enemy enemy, StatusWindow statusWindow, ExplorationManager explorationManager, Location location)
        {
            Player = player;
            Enemy = enemy;
            this.statusWindow = statusWindow;
            this.explorationManager = explorationManager; // explorationManager 초기화
            this.location = location; // location 초기화
        }

        public static void StartBattle(Player player, Enemy enemy, List<Quest> quests, StatusWindow statusWindow, ExplorationManager explorationManager, Location location)
        {
            Console.WriteLine($" 전투 시작! {enemy.Name}이(가) 등장했다!");
            DisplayBattleStatus(player, enemy, statusWindow);

            while (player.Health > 0 && enemy.Health > 0)
            {
                if (!PerformPlayerAction(player, enemy, statusWindow)) return; // 도망 시 전투 종료

                if (enemy.Health > 0)
                {
                    int enemyDamage = Math.Max(0, enemy.AttackPower - player.Defense);
                    player.Health -= enemyDamage;

                    Console.WriteLine($" {enemy.Name}이(가) {player.Name}을(를) 공격했다! {enemyDamage}의 피해를 입었다. (현재 체력: {Math.Max(0, player.Health)})");
                }
            }

            CheckBattleOutcome(player, enemy, quests);
            NextStage(player, explorationManager, location); // explorationManager와 location 전달

        }

        private static bool PerformPlayerAction(Player player, Enemy enemy, StatusWindow statusWindow)
        {
            while (true) // 올바른 입력이 나올 때까지 반복
            {
                Console.WriteLine("\n 선택: 1. 공격  2. 도망");
                Console.Write("입력 >> ");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    var totalStats = statusWindow.GetTotalStats(player);
                    int totalAttack = totalStats.AttackPower;

                    int damageDealt = Math.Max(0, totalAttack - enemy.Defense);
                    enemy.Health -= damageDealt;

                    Console.WriteLine($" {player.Name}이(가) {enemy.Name}을(를) 공격! {damageDealt}의 피해를 입혔다. (적 체력: {Math.Max(0, enemy.Health)})");
                    return true; // 전투 계속 진행
                }
                else if (action == "2")
                {
                    Console.WriteLine(" 도망쳤다!");
                    Console.ReadLine();
                    return false; // 전투 종료
                }
                else
                {
                    Console.WriteLine("! 잘못된 입력입니다. 다시 입력해주세요.");
                    Console.ReadLine();
                }
            }
        }


        private static void CheckBattleOutcome(Player player, Enemy enemy, List<Quest> quests)
        {
            if (player.Health <= 0)
            {
                Console.WriteLine("당신은 죽었습니다. 아무 키나 누르면 종료되며 새 인생을 시작 할 수 있습니다....");
                
                Environment.Exit(0);
            }
            else if (enemy.Health <= 0)
            {
                Console.WriteLine($" 승리하였습니다! 경험치 {enemy.ExpReward} 획득!");
                Console.ReadLine();
                player.GainExp(enemy.ExpReward);
                UpdateQuestProgress(enemy, quests);
            }
        }

        private static void UpdateQuestProgress(Enemy enemy, List<Quest> quests)
        {
            var completedQuests = new List<Quest>();

            foreach (var quest in quests)
            {
                if (quest.Target == enemy.Name)
                {
                    quest.IncrementProgress();
                    Console.WriteLine($" 퀘스트 진행: {quest.Title} ({quest.Progress}/{quest.Goal})");

                    if (quest.IsCompleted)
                    {
                        Console.WriteLine($" 퀘스트 '{quest.Title}' 완료! 보상 {quest.Reward} 골드 지급.");
                        completedQuests.Add(quest); // Mark quest for removal
                    }
                }
            }

            foreach (var quest in completedQuests)
            {
                quests.Remove(quest); // Remove completed quests
            }
        }

        private static void RemoveQuests(List<Quest> quests)
        {
            quests.RemoveAll(quest => quest.IsCompleted);
        }

        private static void DisplayBattleStatus(Player player, Enemy enemy, StatusWindow statusWindow)
        {
            var totalStats = statusWindow.GetTotalStats(player); // 총 능력치 가져오기
            Console.WriteLine($" {player.Name} 체력: {totalStats.Health} | 공격력: {totalStats.AttackPower} | 방어력: {totalStats.Defense} |");
            Console.WriteLine($" {enemy.Name} 체력: {enemy.Health} | 공격력: {enemy.AttackPower} | 방어력: {enemy.Defense} |");
        }


        public static void NextStage(Player player, ExplorationManager explorationManager, Location location)
        {
            if (location == null)
            {
                Console.WriteLine("마을로 돌아갑니다...");
                Console.Write("입력 >> ");

                return;
            }

            Console.WriteLine("다음 스테이지로 이동합니다.");

            while (true)
            {
                Console.WriteLine("1. 계속 나아간다.  2. 마을로 돌아가기");
                Console.Write("입력 >> ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("앞으로 나아갔다..");

                    switch (location.Name)
                    {
                        case "숲":
                            explorationManager.ExploreForest(location);
                            break;
                        case "사막":
                            explorationManager.ExploreDesert(location);
                            break;
                        default:
                            Console.WriteLine("더 이상 진행할 수 없습니다.");
                            break;
                    }

                    break;
                }
                else if (input == "2")
                {
                    Console.WriteLine("마을로 돌아갑니다.");
                    break;
                }
                else
                {
                    Console.WriteLine("! 잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }

    }
}
