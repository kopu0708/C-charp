using System;

class Program
{
    static void Main()
    {
        int[] arr = { 1, 2, 3 };

        try
        {
            for(int i = 0; i<5; i++)
            {
                Console.WriteLine(arr[i]); //i가 3이 되면 IndexOutOfRangeException 이라는 객체가 던져지고 
            }
        }

        catch (IndexOutOfRangeException e) //그걸 여기서 받는다. 
        {
            Console.WriteLine($"예외가 발생했습니다 : {e.Message}");
        }

        Console.WriteLine("종료");
    }
}