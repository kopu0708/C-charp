using System;

class Character //캐릭터 생성을 구현한다고 가정해보자 
{
   
    public string Name { get; init; } = "Unknown"; // 이름과 직업을 한 번 정하면 바꿀 수 없다.
    public string Job { get; init; } = "백수"; 
    public DateTime CreateTime { get; } = DateTime.Now; 

   //이제는 생성자도 필요없다.
}

class Program
{
    static void Main()
    {
        Character character = new Character() { Name = Console.ReadLine(), Job = Console.ReadLine() };
        //이렇게 식으로 값을 받아 객체 생서시점에서만 한번 초기화 된다.


        Console.WriteLine($"캐릭터 생성 완료! {character.Name}, {character.CreateTime},{character.Job}");
    }
}