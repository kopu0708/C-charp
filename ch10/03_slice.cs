using System;

class Program
{
    static void PrintArray(Array array)
    {
        foreach (var e in array)
        {
            Console.Write(e);
            Console.WriteLine();
        }
    }

    static void Main()
    {
        char[] array = new char['Z' - 'A' + 1]; //Z는 아스키 코드로 90, A는 65이다. 즉 이 식은 90 - 65 + 1= 26이다. 
        for (int i = 0; i < array.Length; i++)
            array[i] = (char)('A' + i);

        PrintArray(array[..]); //0부터 끝까지
        PrintArray(array[5..]); //5번째 부터 끝까지

        Range range_5_10 = 5..10;
        PrintArray(array[range_5_10]); //5에서 9까지 

        Index last = ^0;
        Range range_5_last = 5..last; //Range를 생성할 떄 리터럴과 index객체를 함께 사용할 수 있다.
        PrintArray(array[range_5_last]);//5~ 끝까지 

        PrintArray(array[^4..^1]); // 끝에서 4번째부터 끝(^)에서 2번쨰까지 
    }
}