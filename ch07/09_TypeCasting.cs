using System;

class Human()
{
    public void IsHuman()
    {
        Console.WriteLine("I am Human");
    }
}

class Korean : Human
{
    public void Kimchi()
    {
        Console.WriteLine("I like Kimchi");
    }
}

class Japanese : Human
{
    public void Sushi()
    {
        Console.WriteLine("I like Sushi");
    }
}

class MainApp
{
    static void Main(string[] args)
    {
        Human human = new Korean(); //업캐스팅 Korean객체를 만들었지만 Korean의 멤버에는 접근 못하는 상태
        Korean koreanguy; // 변수 선언

        if(human is Korean) //이 인간이 한국인 이라면? 
        {
            koreanguy = (Korean)human; //다운캐스팅 얘 한국인 맞음 ㅇㅇ 
            koreanguy.Kimchi(); //이제 Korean의 메소드를 사용가능 하다. 
        }

        Human human2 = new Japanese();

        Japanese japanese = human2 as Japanese; //다운캐스팅 as를 쓰면 성능적으로 조금 더 유리하다 
        if(japanese != null) //이 인간은 일본인 인가? as는 예외처리를 던지지 않고 참조에 null을 넣어준다.
        {
            japanese.Sushi();
        }

        Japanese japanese2 = human as Japanese; //다운캐스팅 하지만 human은 Korean 객체이기 때문에 null값이 들어감
        if(japanese2 != null)
        {
            japanese2.Sushi();
        }
        else //자연스럽게 else문으로 넘어옴 
        {
            Console.WriteLine("이 사람은 일본인이 아니였네요.");
        }
    }
}
