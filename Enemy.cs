using System;

class Enemy
{
    public string Name { get; private set; }
    public int Health { get;  set; }
    public int AttackPower { get;  set; }
    public int Defense { get;  set; }    
    public int ExpReward { get; private set; }

    public Enemy(string name, int health, int attackPower, int expReward, int defense)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
        Defense = defense;
        ExpReward = expReward;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"{Name}�� ü���� {damage} �����ߴ�! (���� ü��: {Health})");
    }

    public void Attack(Player player)
    {
        Console.WriteLine($"{Name}��(��) {player.Name}��(��) �����ߴ�! {AttackPower}�� ���ظ� ������.");
    }
}
