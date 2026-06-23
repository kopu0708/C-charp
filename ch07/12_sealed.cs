using System;

// 1세대 (조부모): 게임 엔진의 아주 기본적인 물리 객체
class PhysicsEntity
{
    // "상속받는 객체들아, 낙하 로직은 알아서들 구현해라!"
    public virtual void Fall()
    {
        Console.WriteLine("기본적인 중력 적용");
    }
}

// 2세대 (부모): 핵심 퍼즐 블록 시스템
class CorePuzzleBlock : PhysicsEntity
{
    // 여기서 엔진의 Fall()을 오버라이딩해서 정밀한 퍼즐용 물리 로직을 완성함.
    //  sealed 추가: 여길 건드리면 게임이 터져버림 
    public sealed override void Fall()
    {
        Console.WriteLine("그리드 스냅핑 및 퍼즐 블록 전용 정밀 낙하 로직 실행!");
    }
}

// 3세대 (자식): 나중에 추가로 만든 특수 블록
class IceBlock : CorePuzzleBlock
{
    //  컴파일 에러 발생
    // 부모(CorePuzzleBlock)가 Fall()을 sealed로 꽉 잠가버렸기 때문에 더 이상 오버라이딩 불가
    /*
    public override void Fall() 
    {
        Console.WriteLine("얼음 블록 미끄러지기!");
    }
    */

    // 대신 다른 고유한 기능을 추가하는 방식으로 개발을 유도함
    public void Slide()
    {
        Console.WriteLine("얼음 블록만의 미끄러지기 기능 실행!");
    }
}

class MainApp
{
    static void Main()
    {
        IceBlock block = new IceBlock();

        // IceBlock은 자기만의 Fall()을 만들지 못하고, 부모가 봉인해 둔 안전한 낙하 로직을 강제로 쓰게 됨
        block.Fall();
        block.Slide();
    }
}