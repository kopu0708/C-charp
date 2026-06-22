using System;
using System.Globalization;

class Base
{
    protected string Name;
    public Base(string Name) //생성자 선언 
    {
        this.Name = Name; //이건 전에 했던 this Name멤버를 매개변수로 초기화 
        Console.WriteLine($"{this.Name}.Base()"); 
    }

    public void BaseMethod()
    {
        Console.WriteLine($"{Name}.BaseMethod()");
    }
}

class Derived : Base //이게 상속받는 방법  class 상속_클래스명 : 기반_클래스
{
    public Derived(string Name):base(Name) //기반 클래스에 생성자가 매개변수를 받기 때문에 상속 클래스의 생성자에서 base() 생략 불가능  
    {
        Console.WriteLine($"{this.Name}.Derived()");
    }

    public void DerivedMethod()
    {
        Console.WriteLine($"{Name}.DerivedMethod()");
    }
}

class MainApp
{
    static void Main(string[] args)
    {
        Base a = new Base("a");
        a.BaseMethod();

        Derived b = new Derived("b"); //여기 실행결과를 보면 Base가 먼저 실행되는 걸 볼 수 있다. 내부적으로 기반 객체가 먼저 나온다는 소리 
        b.BaseMethod();
        b.DerivedMethod();
    }
}
