using System;
using System.Runtime.CompilerServices;

readonly struct ReadOnlyStruct
{
    public readonly byte R;
    public readonly byte G;
    public readonly byte B;
    private readonly byte Bright;
    public ReadOnlyStruct(byte r, byte g, byte b, byte Bright) //읽기 전용 구조체
    {
        R = r;
        G = g;
        B = b;
        this.Bright = Bright; //그냥 만들어봄 
    }
}

class Program
{
    static void Main()
    {
        byte bright = byte.Parse(Console.ReadLine());
        ReadOnlyStruct monitor = new ReadOnlyStruct(255, 0, 0, bright);

        monitor.B = 100; //컴파일 에러 읽기전용 이기 때문에 생성자로 초기화 이후에는 수정 불가 

        Console.WriteLine($"{monitor.R},{monitor.G},{monitor.B},{monitor.Bright}"); //여기도 컴파일 에러 private로 선언해서 접근안됨 
    }
}