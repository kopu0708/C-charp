using System;
using System.Collections; //생각해보니 지금 굳이 제네릭으로 만들 필요는 없는 듯 어차피 나중에 다 할건데 

enum Block {Square = 1, Line = 2, L = 3, T = 4 }
class Program
{
    static void Main() //테트리스 블럭 리스트를 만들어 볼까 
    { 
        Queue blockList = new Queue();

        //블록 추가 (스폰 순서대로 뒤에 쌓임)
        blockList.Enqueue(Block.Square);
        blockList.Enqueue(Block.Line);
        blockList.Enqueue(Block.L);
        blockList.Enqueue(Block.T);

        // 다음에 나올 블록 미리 보기 
        Console.WriteLine($"다음 블록: {blockList.Peek()}");

        // 블록을 하나씩 꺼내서 처리 (선입선출)
        while (blockList.Count > 0)
        {
            Block current = (Block)blockList.Dequeue(); //보다시피 제네릭이 아니라 자료형을 명시해줘야한다. 
            Console.WriteLine($"현재 블록: {current}");
        }
    }

}