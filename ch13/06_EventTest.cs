using System;

delegate void OnDeath(string monsterName); //일단 대리자 만들고


class Monster
{
    public event OnDeath onDeath; // event 한정자로 수식해서 대리자의 인스턴스를 선언
    public string Name { get; set; }
    
    public void Die()
    {
        Console.WriteLine($"{Name} 사망!");
        onDeath?.Invoke(Name);
    }
}
class Program
{
    static void GiveExp(string name) => Console.WriteLine($"{name} 처치! 경험치 획득");
    static void UpdateQuest(string name) => Console.WriteLine($"퀘스트 갱신: {name}");
    static void DropItem(string name) => Console.WriteLine($"{name} 아이템 드랍!");

    static void Main()
    {
        Monster goblin = new Monster { Name = "고블린" };

        goblin.onDeath += GiveExp;
        goblin.onDeath += UpdateQuest;
        goblin.onDeath += DropItem;

        goblin.Die();  // 3개 다 실행됨
    }
}