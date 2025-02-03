using System;
using System.Collections.Generic;

namespace Textrpg
{
    class BattleManager
    {
        public static void StartBattle(Player player, Enemy enemy, List<Quest> quests)
        {
            Console.WriteLine($"⚔️⚔️⚔️ 전투 시작! {enemy.Name}이(가) 등장했다!");
            DisplayBattleStatus(player, enemy);

            while (player.Health > 0 && enemy.Health > 0)
            {
                if (!PerformPlayerAction(player, enemy)) return; // 도망 시 전투 종료

                if (enemy.Health > 0)
                {
                    enemy.Attack(player);
                    Console.WriteLine($"💥 {enemy.Name}이(가) {player.Name}에게 공격! 현재 체력: {player.Health}");
                }
            }

            CheckBattleOutcome(player, enemy, quests);
        }

        private static bool PerformPlayerAction(Player player, Enemy enemy)
        {
            Console.WriteLine("\n🎮 선택: 1. 공격  2. 도망");
            Console.Write("입력 >> ");
            string action = Console.ReadLine();

            if (action == "1")
            {
                player.Attack(enemy);
                Console.WriteLine($"🗡️ {player.Name}이(가) {enemy.Name}을(를) 공격! 적 체력: {enemy.Health}");
                return true;
            }
            else if (action == "2")
            {
                Console.WriteLine("🏃‍♂️ 도망쳤다!");
                return false;
            }
            else
            {
                Console.WriteLine("⚠️ 잘못된 입력입니다.");
                return PerformPlayerAction(player, enemy); // 올바른 입력을 받을 때까지 반복
            }
        }

        private static void CheckBattleOutcome(Player player, Enemy enemy, List<Quest> quests)
        {
            if (player.Health <= 0)
            {
                Console.WriteLine("☠️ 패배하였습니다...");
            }
            else if (enemy.Health <= 0)
            {
                Console.WriteLine($"🎉 승리하였습니다! 경험치 {enemy.ExpReward} 획득!");
                player.GainExp(enemy.ExpReward);
                UpdateQuestProgress(enemy, quests);
            }
        }

        private static void UpdateQuestProgress(Enemy enemy, List<Quest> quests)
        {
            foreach (var quest in quests)
            {
                if (quest.Target == enemy.Name)
                {
                    quest.IncrementProgress();  // Progress++ 대신 메서드 사용
                    Console.WriteLine($"📜 퀘스트 진행: {quest.Title} ({quest.Progress}/{quest.Goal})");

                    if (quest.IsCompleted)
                    {
                        Console.WriteLine($"✅ 퀘스트 '{quest.Title}' 완료! 보상 {quest.Reward} 골드 지급.");
                    }
                }
            }
        }

        private static void DisplayBattleStatus(Player player, Enemy enemy)
        {
            Console.WriteLine($"👤 {player.Name} 체력: {player.Health} | 🛡️ 방어력: {player.Defense}");
            Console.WriteLine($"👹 {enemy.Name} 체력: {enemy.Health} | ⚔️ 공격력: {enemy.AttackPower}");
        }
    }
}
