using System;

interface ILogger //인터페이스 이름은 I로 시작하는게 관례 
{
    void WriteLog(string message); //어떤 메소드가 들어갈지 미리 선언
}

class ConsoleLogger : ILogger //인터페이스를 상속받아 만든 클래스 
{
    public void WriteLog(string message) //선언해둔 메소드를 구현해야한다. 
    {
        Console.WriteLine("{0} {1}", DateTime.Now.ToLocalTime(), message);
    }
}

class Program
{
    static void Main()
    {
        ILogger logger = new ConsoleLogger(); //참조를 만들고 거기에 파생 클래스의 객체의 위치를 담았다.

        logger.WriteLog("Hello, World!");
    }
}