using System;
using System.Collections.Generic; //앞으로 컬렉션을 쓸건데 필요한거다.

class Program
{
    static void Main()
    {
        List<int> list = new List<int>(); //다음과 같이 <>내부에 자료형을 써줘야한다. 정수형을 담는 크기가 변하는 배열을 선언한거다.

        for(int i = 0; i<5; i++)
        {
            list.Add(i); //이러한 메소드는 동일하다.
        }

       foreach (int n in list)
        {
            Console.WriteLine($"{n}");
        }


       for(int i = 4; i >= 0; i--)
        {
            list.Remove(i);
            foreach (int ints in list)
            {
                Console.WriteLine($"{list[ints]}");
            }
        }

        for (int i = 0; i < 5; i++)
        {
            list.Add(i); 
        }

        list.Remove(2);

        list.Insert(2, 2323);

        foreach(int ints in list)
        {
            Console.WriteLine($"{ints}");
        }
    }
}