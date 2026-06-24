using System;

struct Map3D //struct키워드로 구조체 선언
{
    public int X;
    public int Y;
    public int Z;

    public Map3D(int X, int Y, int Z) 
    {
        this.X = X;
        this.Y = Y;
        this.Z = Z;
    }
}

class Program
{
    static void Main()
    {
        Map3D map1; // 선언만으로 인스턴스가 생성된다.
        map1.X = 10;
        map1.Y = 20;
        map1.Z = 30;

        Console.WriteLine($"{map1.X}, {map1.Y}, {map1.Z}");

        Map3D map2 = new Map3D(100, 200, 300); // 생성자를 이용한 인스턴스 생성도 가능하다.

        Map3D copyMap = map2; //이렇게만 해도 깊은 복사가 됨 (다만 저장공간이 다름 구조체이기 때문에 복사본도 이 선언한 블럭이 끝나면 사라짐)

        copyMap.Z = 400;

        Console.WriteLine($"{map2.X}, {map2.Y}, {map2.Z}");
        Console.WriteLine($"{copyMap.X}, {copyMap.Y}, {copyMap.Z}");
    }
}