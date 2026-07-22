using System;

class Program
{

    static void Main(string[] args)
    {
        Func<int> func1 = () => 10; // 바로 10을 반환 
        Console.WriteLine($"func1() : {func1()}"); 

        Func<int, int> func2 = (x) => x * 2; // 정수형을 받아 정수형으로 반환 
        Console.WriteLine($"func2(4) : {func2(4)}");

        Func<int, int, string> func3 = (x, y) => (x + y).ToString(); // 정수형을 더해서 문자열로 반환하기에 형 변환을 해줘야 한다. 
        Console.WriteLine($"func3(22,7) : {func3(22, 7)}");
    }
}