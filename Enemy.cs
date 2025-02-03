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
        Console.WriteLine($"{Name}의 체력이 {damage} 감소했다! (남은 체력: {Health})");
    }

    public void Attack(Player player)
    {
        Console.WriteLine($"{Name}이(가) {player.Name}을(를) 공격했다! {AttackPower}의 피해를 입혔다.");
    }
}
