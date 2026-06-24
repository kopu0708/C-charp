using System;

public static class IntegerExtension //클래스에 static 수식 
{
    public static int Square(this int myInt) //제곱 기능을 넣음  this 대상 형식(여기선 int) 식별자 
    {
        return myInt * myInt;
    }

    public static int Power(this int myInt, int exponent) //거듭제곱 기능 this 대상 형식(여기선 int) 식별자 , 매개변수 목록

    {
        int result = myInt;
        for(int i = 1; i < exponent; i++)
        {
            result = result * myInt;
        }
        return result;
    }
}

public static class StringExtension //string 확장 메소드 
{
    public static string Append(this string myString, string append)
    {
       string newString = myString += append;

       return newString;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine($"3^2 : {3.Square()}"); // 별도의 인스턴스 생성 없이 원래 있던 메소드 처럼 사용가능해짐 
        Console.WriteLine($"3^4 : {3.Power(4)}");
        Console.WriteLine($"2^10 : {2.Power(10)}");
        string hello = "Hello";
        Console.WriteLine(hello.Append(",World")); //마찬가지로 위에서 정의한 확장 메소드 닷 연산자로 바로 이용가능 
    }
}