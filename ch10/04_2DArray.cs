using System;

class Program //이번에는 2D 타일맵을 가진 게임에서 그 타일의 정보를 가져오는 로직을 짜야한다고 가정해보자
{
    enum Tile { Empty = 0, Orc = 1, Goblin  = 2 } // 1은 오크를 말하고 0은 아무것도 없고 2는 고블린이 있다는 정보를 담고 있다고 가정한다.
    static void Main()
    {
        Tile[,] tileMap = new Tile[3, 3] // 3 X 3 타일맵 열거형 만들어서 
        {
            { Tile.Orc,   Tile.Orc,    Tile.Orc    },
            { Tile.Orc,   Tile.Empty,  Tile.Orc    },
            { Tile.Orc,   Tile.Empty,  Tile.Goblin }
        };

        Console.WriteLine(tileMap[1, 1]); // 비어있는 타일이 나올 것이다. 이런 식으로 행과 열을 []사이에 넣어주면 된다.

        for(int i = 0; i<tileMap.GetLength(0); i++) // 전체를 다 돌기 위해서는 2중 for문을 써야한다. 물론 foreach를 써도 되긴 한다.
        {
            for(int j = 0; j<tileMap.GetLength(1); j++)
            {
                switch (tileMap[i, j])  //각 타일의 정보가 나올것이다.
                {
                    case Tile.Empty: Console.WriteLine("아무것도 없다."); break;
                    case Tile.Orc: Console.WriteLine("무썡긴 오크가 있다."); break;
                    case Tile.Goblin: Console.WriteLine("날 닮은 고블린이 있다."); break;
                    default: Console.WriteLine($"알 수 없는 타일: {tileMap[i, j]}"); break;
                }
            }
        }
    }
}