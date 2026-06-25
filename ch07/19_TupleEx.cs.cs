using System;

class Program
{
    static void Main()
    {
        var a = ("아이", 999); //명명되지 않은 튜플
        var b = (Feel: "신나", num: 220); //명명된 튜플
        var (Feel, num) = ("좋아", 3030);//분해 
        var c = ("싫어", 30);

        //명명된 튜플과 되지않은 튜플 할당
        c = b;
        //이렇게도 되네 
        c = (Feel,num);

        Console.WriteLine($"{a.Item1}, {b.Feel}"); //명명된 튜플과 명명되지 않은 튜플의 호출방식이 다르다.
        Console.WriteLine($"{a.Item1}, {c.Item1}"); //자료형과 수가 같아서 할당된거 확인


    }
}