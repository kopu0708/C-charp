using System;
using System.Numerics;

class createPlayer
{
    public string Name;
    public int Level;
    public int HP;

    public createPlayer(string Name, int Level, int HP) //기본 생성자 다 입력받음
    {
        this.Name = Name;
        this.Level = Level;
        this.HP = HP;
    }

    public createPlayer(string Name) : this(Name, 1, 100)
    {

    }

    public createPlayer() : this("초보자",1,100)
    {

    }
}

class Program
{
    static void Main()
    {
        
        createPlayer p = new createPlayer("레이너");

        Console.WriteLine($"최종 결과: 이름={p.Name}, HP={p.HP}");

        createPlayer p2 = new createPlayer();
        Console.WriteLine($"최종 결과: 이름={p2.Name}, HP={p2.HP}");

        createPlayer p3 = new createPlayer("아아아", 3, 400);
        Console.WriteLine($"최종 결과: 이름={p3.Name}, HP={p3.HP}");

    }
}
