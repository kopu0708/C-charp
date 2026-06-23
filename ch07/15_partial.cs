using System;

partial class MyClass
{
    public void Method1()
    {
        Console.WriteLine("Method1");
    }
}

partial class MyClass
{
    public void Method2()
    {
        Console.WriteLine("Method2");
    }
}

partial class MyClass
{
    public void Method3()
    {
        Console.WriteLine("Method3");
    }
}

class Program
{
    static void Main()
    {
        MyClass my = new MyClass();

        my.Method1();
        my.Method2();
        my.Method3();
    }
}