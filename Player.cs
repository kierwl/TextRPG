using System;

class Player
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int AttackPower { get; private set; }
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int ExpToNextLevel { get; private set; }
    public int Defense { get; private set; }

    public Player(string name)
    {
        Name = name;
        Health = 100;
        AttackPower = 10;
        Level = 1;
        Experience = 0;
        ExpToNextLevel = 20;
        Defense = 2;
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
        Health = 100;
        Console.WriteLine($"���� ��! ���� ����: {Level}, ���ݷ� ����: {AttackPower}");
    }

    public void Attack(Enemy enemy)
    {
        Console.WriteLine($"{Name}��(��) {enemy.Name}��(��) �����ߴ�! {AttackPower}�� ���ظ� ������.");
        enemy.TakeDamage(AttackPower);
    }
}
