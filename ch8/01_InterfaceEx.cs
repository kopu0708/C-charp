using System;

interface ILogger //인터페이스 이름은 I로 시작하는게 관례 
{
    void WriteLog(string message); //어떤 메소드가 들어갈지 미리 선언
}

class ConsoleLogger : ILogger
{
    public void WriteLog(string message)
    {
        Console.WriteLine("{0} {1}", DateTime.Now.ToLocalTime(), message);
    }
}

class Program
{
    static void Main()
    {
        ILogger logger = new ConsoleLogger();

        logger.WriteLog("Hello, World!");
    }
}