using System;
using System.Numerics;

class ReadOnly
{
    private readonly int min;
    private readonly int max;

    public ReadOnly(int v1, int v2) //매개변수로 값을 초기화 하자 
    {
        min = v1; //이렇게 생성자 안에서만 값을 초기화 할 수 있다.
        max = v2;
    }

   /* public void ChangeMax(int newMax)
    {
        max = newMax;  요건 오류 생성자가 아닌 다른 곳은 에러가 발생
    }*/
}

class MainApp
{
    static void Main()
    {
        ReadOnly c = new ReadOnly(100, 10);
    }
}