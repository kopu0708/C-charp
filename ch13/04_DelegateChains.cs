using System;


delegate void OnDeath(string monsterName);
class Program
{
    static void GiveExp(string name)
    {
        Console.WriteLine("{0}을 처치 경험지 100 획득", name);
    }

    static void KillLog(string name)
    {
        Console.WriteLine("User가 {0}을 처치했습니다.", name);
    }

    static void UpdateQuest(string name)
    {
        Console.WriteLine($"퀘스트 진행도 갱신: {name}처지");
    }

    static void DropItem(string name)
    {
        Console.WriteLine($"{name}가 아이템을 드랍했습니다!");
    }


    static void Main()
    {
        OnDeath onMonsterDeath = GiveExp;
        onMonsterDeath += KillLog;
        onMonsterDeath += UpdateQuest;
        onMonsterDeath += DropItem;

        onMonsterDeath("고블린");

        onMonsterDeath -= DropItem;  // 이번 몬스터는 아이템 드랍 안함
        onMonsterDeath("슬라임");
    }
}