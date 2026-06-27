using System;

class Charater //캐릭터 생성을 구현한다고 가정해보자 
{
    private string name; //캐릭터 이름
    private DateTime createTime; //캐릭터 생성 시간을 저장해두고 싶다.
    private string job; // 캐릭터 직업 

    public Charater(string name, string job) //생성자를 만들었다. 
    {
        this.name = name;
        this.createTime = DateTime.Now;
        this.job = job;
    }
    public string Name
    {
        get 
        {
            return name; //누가 이름을 물어보면 말해줘야한다. (그게 예의니깐)
        }

        set
        {
            name = value; //value는 누구도 선언하지 않았지만 암묵적으로 매개변수로 간주한다 했었다.
        }
    }

    public DateTime CreateTime //set이 굳이 필요없다.
    {
        get
        {
            return createTime;
        }
    }

    public string Job
    {
        get
        {
            return job;
        }

        set
        {
            job = value;
        }
    }  
}

class Program
{
    static void Main()
    {
        string name = Console.ReadLine(); //이름을 입력받는다.
        string job = Console.ReadLine(); //직업을 입력받자 
        Charater charater = new Charater(name, job); //생성자로 넘겨주자 

        Console.WriteLine($"캐릭터 생성 완료! {charater.Name}, {charater.CreateTime},{charater.Job}");//프로퍼티로 인해 일반 변수처럼 접근이 가능하다.
    }
}