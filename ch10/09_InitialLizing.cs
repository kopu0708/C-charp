using System;
using System.Collections; //생각해보니 지금 굳이 제네릭으로 만들 필요는 없는 듯 어차피 나중에 다 할건데 

class Program
{
    static void Main()
    {
        int[] arr = { 123, 456, 789 };

        ArrayList arrayList = new ArrayList(arr);
        foreach (object item in arrayList)
        {
            Console.WriteLine($"ArrayList : {item}");
        }
        Console.WriteLine();

        Stack stack = new Stack(arr);
        foreach(object item in stack)
        {
            Console.WriteLine($"Stack : {item}");
        }
        Console.WriteLine();

        Queue queue = new Queue(arr);
        foreach(object item in queue)
        {
            Console.WriteLine($"Queue : {item}");
        }
        Console.WriteLine();

        ArrayList list2 = new ArrayList() { 11, 22, 33 };
        foreach(object item in list2)
        {
            Console.WriteLine($"ArrayList2 : {item}");
        }
        Console.WriteLine();
    }
}