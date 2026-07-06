using System;
using System.Collections;
using System.Threading;


enum monster  { Empty = 0, Orc = 1, Goblin = 2 }
class MainApp
{
    static void Main() //이번에는 각 스테이지 마다 다른 배열 크기를 가진 몬스터 리스트를 만들어 보자.
    {
        int[][] stages = new int[3][]; //가변 배열은 []을 두번 써서 선언한다. 
        stages[0] = new int[] { 1, 2 }; //스테이지1은 오크와 고블린이 각 한마리 씩 나온다.
        stages[1] = new int[] { 1, 1, 2, 2, 2, 2 }; // 스테이지2 오크 2 고블린 4
        stages[2] = new int[] { 1, 1, 1, 1, 1 }; //오크 5마리 

        //이처럼 각 행마다 길이가 다르다는 것을 볼 수 있다.

        for (int i = 0; i < stages.Length; i++)
        {
            Console.WriteLine($"--- 스테이지 {i + 1} ---");
            for (int j = 0; j < stages[i].Length; j++) //가변 배열은 배열의 배열이라 Length를 써야한다.
            {
                switch (stages[i][j]) //가변 배열의 다음과 같이 접근해야한다.
                {
                    case (int)monster.Empty: Console.WriteLine("아무것도 없다."); break;
                    case (int)monster.Orc: Console.WriteLine("무썡긴 오크가 있다."); break;
                    case (int)monster.Goblin: Console.WriteLine("날 닮은 고블린이 있다."); break;
                    default: Console.WriteLine($"알 수 없는 몬스터: {stages[i][j]}"); break;
                }
            }
        }

    }
}