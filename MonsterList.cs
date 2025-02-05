using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textrpg
{
    class Slime : Enemy
    {
        public Slime() : base("슬라임", 30, 5, 2, 10) { }

        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 점프 공격을 했다!");
            base.Attack(player);
        }
    }

    class Goblin : Enemy
    {
        private Random random = new Random();
        public Goblin() : base("고블린", 20, 10, 5, 15) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 단검으로 찌르기를 했다!");
            base.Attack(player);

            if (random.Next(100) < 30) // 30% 확률로 훔치기 실행
            {
                int stolenGold = Math.Min(20, player.Gold);
                player.Gold -= stolenGold;
                Console.WriteLine($"{Name}이(가) 플레이어의 골드를 훔쳤다! (-{stolenGold} Gold)");
            }
        }
    }

    class Orc : Enemy
    {
        private bool isEnraged = false;
        public Orc() : base("오크", 40, 15, 10, 20) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 도끼 공격을 했다!");
            base.Attack(player);

            if (Health <= 30 && !isEnraged)
            {
                isEnraged = true;
                AttackPower += 5;
                Console.WriteLine($"{Name}이(가) 분노 상태에 돌입했다! (공격력 증가)");
            }
        }
    }

    class Cobolt : Enemy
    {
        private Random random = new Random();
        public Cobolt() : base("코볼트", 60, 20, 15, 25) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 창으로 찔렀다!");
            base.Attack(player);

            if (random.Next(100) <= 50) // 50% 확률로 연속공격 실행
            {
                
                Console.WriteLine($"{Name}의 민첩함으로 인해 두번 공격했다.");
                base.Attack(player);

                if (random.Next(100) < 70) // 30% 확률로 연속공격 실행
                {

                    Console.WriteLine($"{Name}의 민첩함으로 인해 세번 공격했다.");
                    base.Attack(player);
                }

            }

        }
    }
    class Spider : Enemy
    {
        public Spider() : base("거미", 130, 40, 20, 10) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 실풀이를 했다!");
            base.Attack(player);
        }
    }
    class Catus : Enemy
    {
        public Catus() : base("선인장", 100, 30, 15, 25) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 가시공격을 했다!");
            base.Attack(player);
        }
    }
    class Scolpi : Enemy
    {
        public Scolpi() : base("전갈", 150, 50, 40, 30) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 독침을 찌르기를 했다!");
            base.Attack(player);
        }
    }
    class Dragon : Enemy
    {
        private int turnCount = 0;
        public Dragon() : base("드래곤", 500, 100, 200, 50) { }
        public override void Attack(Player player)
        {
            turnCount++;

            if (turnCount % 3 == 0)
            {
                int breathDamage = AttackPower * 2;
                player.Health -= breathDamage;
                Console.WriteLine($"{Name}이(가) 화염 브레스를 사용했다! {breathDamage}의 피해를 입었다!");
            }

            Console.WriteLine($"{Name}이(가) 꼬리로 후두려 맞았다!");
            base.Attack(player);
        }
    }

    class MonsterList // 지역별 몬스터리스트
    {
        private static List<Func<Enemy>> monsters = new List<Func<Enemy>>// 사용안함
        {
            () => new Slime(),
            // 다른 몬스터 클래스도 여기에 추가
            () => new Goblin(),
            () => new Orc(),
            () => new Cobolt()
        };

        private static List<Func<Enemy>> monsters2 = new List<Func<Enemy>> //사용안함
        {
            () => new Spider(),
            // 다른 몬스터 클래스도 여기에 추가
            () => new Catus(),
            () => new Scolpi(),
            () => new Dragon()
        };

        public static Enemy GetRandomMonster() // 숲 지역 몬스터 랜덤 생성
        {
            Random random = new Random();
            int roll = random.Next(100);
            if (roll < 40) return new Slime();
            else if (roll < 75) return new Goblin();
            else if (roll < 95) return new Orc();

            else return new Cobolt(); // 5% 확률로 보스 등장
        }
        public static Enemy GetRandomMonster2() // 사막 지역 몬스터 랜덤 생성
        {
            Random random = new Random();
            int roll = random.Next(100);
            if (roll < 40) return new Spider();
            else if (roll < 80) return new Catus();
            else if (roll < 95) return new Scolpi();
            else return new Dragon(); // 5% 확률로 보스 등장
        }
    }

}
