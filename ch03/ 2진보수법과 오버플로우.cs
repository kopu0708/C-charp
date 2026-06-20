using System;

namespace IntegralTypesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            byte a = 255;
            sbyte b = (sbyte)a;

            Console.WriteLine($"Byte value: {a}");
            Console.WriteLine($"SByte value after conversion: {b}");

            //데이터가 흘러 넘쳐요 

            uint maxUInt = uint.MaxValue;

            Console.WriteLine(maxUInt); //최대값을 넣고요

            maxUInt += 1; //1을 더하면

            Console.WriteLine(maxUInt); // 0이 됩니다.그럼 최저값 보다 작은 수를 넣으면 어떻게 될까요?
            
            uint minUInt = uint.MinValue;
            Console.WriteLine(minUInt); //최소값을 넣고요
            minUInt -= 1; //1을 빼면
            Console.WriteLine(minUInt); //최대값이 됩니다. 다루려는 데이터의 범위와 변수의 형식을 적절히 맞춰줍시다.
        }
    }
}
