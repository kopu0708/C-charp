using System;
using System.Runtime.CompilerServices;

class Myclass
{
    public int MyField1;
    public int MyField2;

     //DeepCopy() 메소드는 다음과 같다
     public Myclass DeepCopy()
     {
        Myclass newCopy = new Myclass();
        newCopy.MyField1 = this.MyField1; 
        newCopy.MyField2 = this.MyField2;
        return newCopy;
     }
}

class Program
{
    static void Main()
    {
        Myclass source = new Myclass(); 
        source.MyField1 = 10;
        source.MyField2 = 20;
        Myclass copy = new Myclass(); //여기서 힙 공간을 만들었겠지만

        copy = source; //이렇게 쓰면 힙 공간과의 연결이 끊어지고 source객체의 참조만 가져온다.
        copy.MyField2 = 30;

        Console.WriteLine("source: {0}, {1}", source.MyField1, source.MyField2); //그러면 얘도 10, 30이 나온다.
        Console.WriteLine("copy: {0}, {1}",copy.MyField1, copy.MyField2);

        Myclass source2 = new Myclass(); 
        source2.MyField1 = 10;
        source2.MyField2 = 20;

        Myclass copy2 = new Myclass();
        copy2.MyField1 = source2.MyField1;
        copy2.MyField2 = source2.MyField2;

        copy2.MyField2 = 30;

        Console.WriteLine("source2: {0}, {1}", source2.MyField1, source2.MyField2); //이 친구는 원본 그대로
        Console.WriteLine("copy2: {0}, {1}", copy2.MyField1, copy2.MyField2);

        //근데 이렇게 매번 쓰면 너무 귀찮으니깐 클래스 내부에 DeepCopy() 메소드를 만들거나 ICloneable 인터페이스를 활용해
        //Clone() 이라는 메서드를 사용한다. DeepCopy 메소드를 위에 적어두겠다.

       

    }

}

