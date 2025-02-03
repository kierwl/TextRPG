using System;
using Textrpg;

class Player
{
    public string Name { get; private set; }
    public int Health { get;  set; }
    public int AttackPower { get;  set; }
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int ExpToNextLevel { get; private set; }
    public int Defense { get;  set; }
    public Job job { get;  set; }
    public Inventory Inventory { get; private set; }
    public Item EquippedItem { get; set; } // ������ ������
    public Player(string name, Job job)
    {
        Name = name;
        Health = 100;
        AttackPower = 10;
        Level = 1;
        Experience = 0;
        ExpToNextLevel = 20;
        Defense = 2;
        this.job = job;
        Inventory = new Inventory();  // �κ��丮 �ʱ�ȭ
        EquippedItem = null;

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
