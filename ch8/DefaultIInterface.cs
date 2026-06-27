using System;
interface IDefaultInterface //기존 레거시 인터페이스
{
    void WriteHellow(string message); // 본래 있던 기능 

    void WriteError(string error) //추가할 메소드 
    {
        WriteHellow($"Error: {error}");  //상속 받은 클래스가 오버라이딩 하지 않으면 실행될 코드 물론 인터페이스가 참조하지 않으면 호출 자체가 안됨
    }
}

class ClassA : IDefaultInterface //여기서는 기본 구현 메소드를 오버라이드 하지 않겠다.
{
    public void WriteHellow(string massage)
    {
        Console.WriteLine(massage);  
    }
}

class ClassB : IDefaultInterface
{
    public void WriteHellow(string massage)
    {
        Console.WriteLine(massage);
    }

    public void WriteError(string error) //여기서 기본 구현 메서드를 오버라이딩 
    {
        Console.WriteLine("기본 구현 메서드랑 다른 메세지");
    }
}

class Program
{
    static void Main()
    {
        IDefaultInterface defaultInterface = new ClassA(); //인터페이스로 참조 
        defaultInterface.WriteHellow("안녕");
        defaultInterface.WriteError("여기선 호출이된다.");

        ClassB classB = new ClassB();
        classB.WriteHellow("안녕");
        classB.WriteError("dkdkdk"); //이것도 되는데? 상속 받고 구현을 했으니깐 된다. 하지만

        ClassA classA = new ClassA();
        //classA.WriteError("kdkdkd"); 이건 컴파일 에러 ClassA 클래스에서 구현하지 않았고 인터페이스로 참조해서 호출하지 않았기 떄문이다. 
    }
}