using System;

class Character //캐릭터 생성을 구현한다고 가정해보자 
{
    //필드를 모두 지워도 괜찮다.
    public string Name { get; set; } = "Unknown"; //이전 예제에서 배웠던 대로 대문자로 시작하자
    public string Job { get; set; } = "백수"; 
    public DateTime CreateTime { get; } = DateTime.Now; // 아까와 동일하게 set은 필요없다 바로 초기화 시켜준 값으로 유지하자

   //이제는 생성자도 필요없다.
}

class Program
{
    static void Main()
    {
        Character character = new Character() // 객체를 생성할 때 원하는 프로퍼티만 초기화 시켜준다.
        {
            Name = Console.ReadLine(),
            Job = Console.ReadLine()
        };
        

        Console.WriteLine($"캐릭터 생성 완료! {character.Name}, {character.CreateTime},{character.Job}");//프로퍼티로 인해 일반 변수처럼 접근이 가능하다.
    }
}