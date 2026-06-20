using System;

class EXclass //이와 같이 class 키워드와 클래스 이름으로 선언한다.
{
   //이곳에 필드와 메소드를 선언
}

//예제

class Cat
{
    public string Name; //클래스 안에 선언된 변수들을 필드 라고 한다. 필드와 메소드를 비롯한 프로퍼티 이벤트를 모두 멤버라고 한다.
    public string Color;

    public void Meow()
    {
        Console.WriteLine("{0} : 야옹", Name);
    }


}

class Program
{
    static void Main()
    {
        Cat Kitty = new Cat(); // 첫번째 고양이 키티 객체 생성 
        Kitty.Color = "white";
        Kitty.Name = "키티";
        Kitty.Meow();
        Console.WriteLine($"{Kitty.Name} : {Kitty.Color}");

        Cat Nero = new Cat(); // 두번쨰 고양이 네로 객체 생성
        Nero.Color = "black";
        Nero.Name = "네로";
        Console.WriteLine($"{Nero.Name} : {Nero.Color}");
    }
}