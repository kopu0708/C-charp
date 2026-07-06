using System;
using System.Collections;

class MyEnumerator : IEnumerable
{
    int[] numbers = { 1, 2, 3, 4 };
    public IEnumerator GetEnumerator()
    {
        yield return numbers[0];
        yield return numbers[1];
        yield return numbers[2];
        yield break; // GetEnumerator() 메소드를 종료시킨다.
        yield return numbers[3]; //따라서 이건 실행안됨
    }
}

class MainApp
{
    static void Main()
    {
        var obj = new MyEnumerator(); 
        foreach(int i in obj)
        {
            Console.WriteLine(i);
        }
    }
}