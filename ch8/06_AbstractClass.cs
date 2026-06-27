using System;

abstract class AbstractBase //abstract을 수식해 추상 클래스 선언 직접 인스턴스 생성은 불가
{
    public void publicMethodA()  //추상 클래스도 일반 메서드를 가질 수 있다. 파생 클래스에 그대로 상속된다.
    {
        Console.WriteLine("AbstractBase.PublicMethodA()"); 
    }

    protected void PrivateMethodA() // protected: 외부에서는 접근 불가(private처럼), 파생 클래스 내부에서는 접근 가능
                                    // private이었다면 Derived에서 PrivateMethodA() 호출 시 컴파일 에러
    {
        Console.WriteLine("AbstractBase.PrivateMethodA()");
    }

    public abstract void AbstractMethodA(); // 추상 메소드 이 클래스를 상속받으면 이것 필수로 오버라이딩 해야함 
}                                           // + abstract 메서드는 자동으로 virtual처럼 동작 — override 키워드가 필요한 이유

class Derived : AbstractBase
{
    public override void AbstractMethodA()  //abstact 키워드는 암묵적으로 virtual이다. 그래서 override 키워드가 필요하다. 

    // abstract → 반드시 override (구현 강제)
    // virtual → override 선택 (기본 구현 있음)
    {
        Console.WriteLine("Derived.AbstractMethodA()");
        PrivateMethodA();
    }
}

class Program
{
    static void Main()
    {
       // AbstractBase abstractBase = new AbstractBase(); 추상 클래스는 인스턴스 생성이 불가능 
        AbstractBase obj = new Derived();
        obj.AbstractMethodA();
        obj.publicMethodA();
    }
}