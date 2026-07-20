using System;

class Program
{
    delegate int Calculate(int a, int b);

    static void Main()
    {
        Calculate calc = (a, b) => a + b; // 매우 간결한 매소드와 매우 간결하게 선언된 모습 

        Console.WriteLine($"{3} + {4} : {calc(3, 4)}");
    }
}