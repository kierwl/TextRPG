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
    public List<Item> equippedItem { get; set; } // ������ ������
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
        Inventory = new Inventory(this);  // �κ��丮 �ʱ�ȭ
        equippedItem = new List<Item>();
        Gold = 1000;

    }
    

    public void GainExp(int exp)
    {
        Experience += exp;
        Console.WriteLine($"{exp} ����ġ�� ������ϴ�!");

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
        Console.WriteLine($"���� ��! ���� ����: {Level}, ���ݷ� ����: {AttackPower}");
    }

    public void Attack(Enemy enemy)
    {
        Console.WriteLine($"{Name}��(��) {enemy.Name}��(��) �����ߴ�! {AttackPower}�� ���ظ� ������.");
        enemy.TakeDamage(AttackPower);
    }
    public void RestoreHealth(int amount)
    {
        Health += amount;
        if (Health > 100) Health = 100;  // �ִ� ü���� 100
    }
}
