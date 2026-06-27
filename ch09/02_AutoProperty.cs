using System;

class Character //캐릭터 생성을 구현한다고 가정해보자 
{
    //필드를 모두 지워도 괜찮다.
    public string Name { get; set; } = "Unknown"; //이전 예제에서 배웠던 대로 대문자로 시작하자
    public string Job { get; set; } = "백수";
    public DateTime CreateTime { get; } = DateTime.Now; // 아까와 동일하게 set은 필요없다 바로 초기화 시켜준 값으로 유지하자

    public Character(string _name, string _job) //생성자를 만들었다.
    {
        Name = _name; //매개변수로 곧장 초기화 하자 
        Job = _job;
    }
}

class Program
{
    static void Main()
    {
        string name = Console.ReadLine(); //이름을 입력받는다.
        string job = Console.ReadLine(); //직업을 입력받자 
        Character charater = new Character(name, job); //생성자로 넘겨주자 

        Console.WriteLine($"캐릭터 생성 완료! {charater.Name}, {charater.CreateTime},{charater.Job}");//프로퍼티로 인해 일반 변수처럼 접근이 가능하다.
    }
}