using System;

class Base
{
    public void MyMethod()
    {
        Console.WriteLine("Base클래스의 메소드");  //원래 메소드 
    }
}

class Derived : Base
{
    public new void MyMethod() //오버라이딩이 아닌 메소드 숨기기 new 키워드가 붙음
    {
        Console.WriteLine("자식클래스의 메소드"); 
    }
}

class MainApp
{
    static void Main()
    {
        Base baseObj = new Base(); //원본 클래스로 객체를 생성했기 때문에 원본 메소드가 실행됨
        baseObj.MyMethod();

        Derived derivedObj = new Derived(); //여기서는 자식 클래스로 객체 생성함 그럼 자식클래스의 메소드가 나옴
        derivedObj.MyMethod();

        Base baseOrDerived = new Derived(); // 내부적으로 new Derived()가 먼저 실행됨 힙 메모리에 먼저 할당 
                                            // 부모클래스의 자리 옆에 자식클래스를 붙여 할당함 
                                            // 그 다음 Base baseOrDerived 가 시행되며 스택에 자리가 생기고 위에서 생긴 힙의 메모리 주소 시작 위치를 저장    
                                            // 업캐스팅은 이로 인해 부모 클래스 부분만 읽을 수 있음 그래서 업캐스팅 상태에서는 자식 클래스의 맴버에 접근 못함
                                             
        baseOrDerived.MyMethod();
    }
}