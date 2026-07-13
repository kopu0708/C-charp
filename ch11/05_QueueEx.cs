using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Queue<int> queue = new Queue<int>();

        queue.Enqueue(1); //큐에 쌓기
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5); //이 친구가 가장 마지막에 나옴

        while (queue.Count > 0)
            Console.WriteLine(queue.Dequeue()); //큐에 있는거 순서대로 꺼내기
    }
}