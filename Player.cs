using System;
using Textrpg;

class Player
{
    public string Name { get; private set; }
    public int Health { get;  set; }
    public int MaxHP { get;  set; }
    public int AttackPower { get;  set; }
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int ExpToNextLevel { get; private set; }
    public int Defense { get;  set; }
    public int Gold { get; set; }
    public Job job { get; private set; }
    public Inventory Inventory { get; private set; }
    public List<Item> equippedItem { get; set; } // 장착된 아이템
    public Player(string name, Job job)
    {
        Name = name;
        Health = 100 + job.HealthBonus; ;
        MaxHP = Health;
        AttackPower = 10 + job.AttackBonus;
        Level = 1;
        Experience = 0;
        ExpToNextLevel = 20;
        Defense = 5 + job.DefenseBonus;
        this.job = job;
        Inventory = new Inventory(this);  // 인벤토리 초기화
        equippedItem = new List<Item>();
        Gold = 1000;

    }
    

    public void GainExp(int exp)
    {
        Experience += exp;
        Console.WriteLine($"{exp} 경험치를 얻었습니다!");

        while (Experience >= ExpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Experience -= ExpToNextLevel;
        Level++;
        ExpToNextLevel += 20;
        AttackPower += 5;
        Defense += 2;
        Health = 100;
        Console.WriteLine($"레벨 업! 현재 레벨: {Level}, 공격력 증가: {AttackPower}");
    }

    public void Attack(Enemy enemy)
    {
        Console.WriteLine($"{Name}이(가) {enemy.Name}을(를) 공격했다! {AttackPower}의 피해를 입혔다.");
        enemy.TakeDamage(AttackPower);
    }
    public void RestoreHealth(int amount)
    {
        Health += amount;
        if (Health > 100) Health = 100;  // 최대 체력은 100
    }
}
