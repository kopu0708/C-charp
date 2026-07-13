using System;
using System.Collections.Generic; //이거 쓸려면 이거 있어야함

class Program
{
    static void Main()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            list.Add(i);
        }

        foreach (int element in list)
            Console.Write($"{element} ");
        Console.WriteLine();

        list.RemoveAt(2);

        foreach (int element in list)
            Console.Write($"{element} ");
        Console.WriteLine();

        list.Insert(2, 2);

        foreach (int element in list)
            Console.Write($"{element} ");
        Console.WriteLine();
    }
}