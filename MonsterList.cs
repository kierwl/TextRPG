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
        public Goblin() : base("고블린", 20, 10, 5, 15) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 단검으로 찌르기를 했다!");
            base.Attack(player);
        }
    }

    class Orc : Enemy
    {
        public Orc() : base("오크", 40, 15, 10, 20) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 도끼 공격을 했다!");
            base.Attack(player);
        }
    }

    class Cobolt : Enemy
    {
        public Cobolt() : base("코볼트", 50, 20, 15, 25) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 창으로 찔렀다!");
            base.Attack(player);
        }
    }
    class Spider : Enemy
    {
        public Spider() : base("거미", 130, 40, 5, 10) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 실풀이를 했다!");
            base.Attack(player);
        }
    }
    class Catus : Enemy
    {
        public Catus() : base("선인장", 100, 30, 10, 25) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 가시공격을 했다!");
            base.Attack(player);
        }
    }
    class Scolpi : Enemy
    {
        public Scolpi() : base("전갈", 150, 50, 20, 30) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 독침을 찌르기를 했다!");
            base.Attack(player);
        }
    }
    class Dragon : Enemy
    {
        public Dragon() : base("드래곤", 500, 100, 20, 50) { }
        public override void Attack(Player player)
        {
            Console.WriteLine($"{Name}이(가) 불을 뿜었다!");
            base.Attack(player);
        }
    }

    class MonsterList // 지역별 몬스터리스트
    {
        private static List<Func<Enemy>> monsters = new List<Func<Enemy>>
        {
            () => new Slime(),
            // 다른 몬스터 클래스도 여기에 추가
            () => new Goblin(),
            () => new Orc(),
            () => new Cobolt()
        };

        private static List<Func<Enemy>> monsters2 = new List<Func<Enemy>>
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
            int index = random.Next(monsters.Count);
            return monsters[index]();
        }
        public static Enemy GetRandomMonster2() // 사막 지역 몬스터 랜덤 생성
        {
            Random random2 = new Random();
            int index = random2.Next(monsters2.Count);
            return monsters2[index]();
        }
    }

}
