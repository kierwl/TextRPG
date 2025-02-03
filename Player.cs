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
        Health = 100;
        Console.WriteLine($"레벨 업! 현재 레벨: {Level}, 공격력 증가: {AttackPower}");
    }

    public void Attack(Enemy enemy)
    {
        Console.WriteLine($"{Name}이(가) {enemy.Name}을(를) 공격했다! {AttackPower}의 피해를 입혔다.");
        enemy.TakeDamage(AttackPower);
    }
}
