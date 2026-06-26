using System;

interface IRunnable
{
    void Run();
}

interface IFlyable
{
    void Fly();
}

class FlyingCar : IRunnable, IFlyable //한번에 상속 받는 법은 그냥 ,로 구분하며 나열하는 것이다.
{
    public void Run() //당연하지만 메소드는 반드시 모두 구현해줘야한다. 
    {
        Console.WriteLine("뛴다! 뛰어!");
    }

    public void Fly()
    {
        Console.WriteLine("난다! 날아!");
    }
}

class Program
{
    static void Main()
    {
        FlyingCar car = new FlyingCar(); //인스턴스를 만든다.
        car.Run(); //클래스의 메서드 실행 
        car.Fly(); 

        IRunnable runnable = car as IRunnable; // 인터페이스의 변수에 클래스의 인스턴스를 할당 as로 형변환 
        runnable.Run();

        IFlyable flyable = car as IFlyable; //여기도 마찬가지 
        flyable.Fly();
    }
}