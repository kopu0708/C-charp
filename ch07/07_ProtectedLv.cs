using System;

class ProtectedLV
{
    public int publicVar = 1;
    protected int protectedVar = 2;
    private int privateVar = 3;

    private void Display() // 같은 클래스 내에서만 접근 가능한 메서드    
    {
        Console.WriteLine("Public Variable: " + publicVar); // 퍼블릭 멤버는 어디서든 접근 가능
        Console.WriteLine("Protected Variable: " + protectedVar); // 프로텍티드 멤버는 같은 클래스나 상속받은 클래스에서 접근 가능
        Console.WriteLine("Private Variable: " + privateVar);  // 프라이빗 멤버는 같은 클래스에서만 접근 가능
    }

    public void Show() // 퍼블릭 메서드로 Display 메서드를 호출할 수 있도록 함
    {
        Display(); // 같은 클래스 내에서 Display 메서드를 호출하여 멤버 변수들을 출력
    }
}

class DerivedClass : ProtectedLV // 이게 상속받은 클래스 
{
    public void Display()
    {
        Console.WriteLine("Public Variable: " + publicVar);
        Console.WriteLine("Protected Variable: " + protectedVar); // 상속받은 클래스에서는 프로텍티드 멤버에 접근 가능
        // Console.WriteLine("Private Variable: " + privateVar); // 에러 프라이빗 멤버는 같은 클래스에서만 접근 가능
    }
}
class Program
{
    static void Main(string[] args)
    {
        ProtectedLV obj = new ProtectedLV();
        Console.WriteLine("Public Variable: " + obj.publicVar);
        // Console.WriteLine("Protected Variable: " + obj.protectedVar); // 에러 프로텍티드 멤버는 같은 클래스나 상속받은 클래스에서만 접근 가능
        // Console.WriteLine("Private Variable: " + obj.privateVar); // 에러 프라이빗 멤버는 같은 클래스에서만 접근 가능

        DerivedClass derivedObj = new DerivedClass();
        obj.Show(); // ProtectedLV 클래스의 Show 메서드를 호출하여 Display 메서드를 통해 멤버 변수들을 출력
        derivedObj.Display(); 
       

    }
}
