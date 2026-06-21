using System;

//예제

class Cat
{
    public string Name; //클래스 안에 선언된 변수들을 필드 라고 한다. 필드와 메소드를 비롯한 프로퍼티 이벤트를 모두 멤버라고 한다.
    public string Color;
    public Cat() //차례대로 한정자 생성자
    {
        Name = "";
        Color = "";
    }
    
    public Cat(string _Name, string _Color) //객체를 생성할 떄 이름과 색을 입력받아 초기화한다.
    {
        Name = _Name;
        Color = _Color;
    }

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

        Cat Nero = new Cat("네로", "검은색"); // 두번쨰 고양이 네로 객체 생성 여기서는 생성자로 인해 객체가 생성되는 즉시 값이 초기화 되서 나온다.
        Nero.Meow();
        Console.WriteLine($"{Nero.Name} : {Nero.Color}");
    }
}
