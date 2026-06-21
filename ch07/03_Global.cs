using System;

class Global
{
    public static int Count = 0; //static 키워드를 붙여서 필드를 생성
}

 class ClassA
{
    public ClassA()
    {
        Global.Count++; //이와 같이 static 멤버에는 별도의 인스턴스 생성 없이 호출 가능
    }
}

class ClassB
{
    public ClassB()
    {
        Global.Count++;
    }
}

class MainApp
{
    static void Main()
    {
        Console.WriteLine($"Global.Count : {Global.Count}");

        new ClassA(); //이 친구들은 인스턴스(객체)를 생성하는 과정(생성자 호출)이기에 new 키워드가 필요함
        new ClassB();

        Console.WriteLine($"Global.Count : {Global.Count}"); //결과는 2
    }
}
