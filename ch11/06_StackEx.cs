using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Stack<int> stack = new Stack<int>();

        stack.Push(1); //push로 넣고 pop으로 뺀다 
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        stack.Push(5); //stack은 마지막에 넣은게 가장 먼저 나옴 

        while (stack.Count > 0)
            Console.WriteLine(stack.Pop());

    }
}
