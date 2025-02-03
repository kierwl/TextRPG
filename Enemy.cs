using System;

class Enemy
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int AttackPower { get; private set; }
    public int ExpReward { get; private set; }

    public Enemy(string name, int health, int attackPower, int expReward)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
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
