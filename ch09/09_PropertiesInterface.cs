using System;

interface IAttackable //공격을 받을 수 있는 것들은 모두 체력이 필요할 것이다.
{
    int Health { get; set; } //체력은 바뀌니깐 
    string Name { get; init; } //이름은 변경되지 않아야 하니깐 
    //실제 게임이라면 여기에 체력이 감소되는 메서드도 있어야 겠구나 
    void TakeDamage(int amount);
}

class Enemy : IAttackable
{
    public int Health { get; set; }
    public string Name { get; init; }
    //그럼 당연히 여기에 체력이 감소되는 메서드를 구현하고
    public void TakeDamage(int amount)
    {
        Health -= amount;
        Console.WriteLine($"{Name}에게 {amount}의 데미지를 입혔습니다. 남은 체력: {Health}");
    }
}

class DestroyableObject : IAttackable
{
    public int Health { get; set; }
    public string Name { get; init; }
    //여기도 마찬가지로 체력이 감소되는 메서드를 구현하고
    public void TakeDamage(int amount)
    {
        Health -= amount;
        Console.WriteLine($"[파괴 가능 오브젝트] {Name}이(가) 파괴되었습니다! 남은 내구도: {Health}");
    }
}

class Program
{
    static void Main()
    {
        Enemy orc = new Enemy() { Health = 200, Name = "무썡긴 오크" };
        DestroyableObject box = new DestroyableObject() { Health = 100, Name = "나무상자" };

        //이렇게도 사용가능하다. 이러면 한번에 데미지 처리 가능함 
        IAttackable[] targets = [orc, box]; 
        foreach (var t in targets)
            t.TakeDamage(100); //임의의 수를 그냥 하드코딩했다.
    }
}