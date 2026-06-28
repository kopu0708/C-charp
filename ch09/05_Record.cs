using System;

record RTtransaction
{
    public string From { get; init; } //초기화 전용 자동 구현 프로퍼티들 
    public string To   { get; init; }
    public int Amount  { get; init; }

    public override string ToString() //이 예제에서는 ToString을 오버라이드 해서 쓴다. 
    {
        return $"{From,-10} -> {To,-10} : ${Amount}"; //-10은 왼쪽 정렬을 의미한다 10칸을 가지라는 뜻 
    }
}

class Program
{
    static void Main()
    {
        RTtransaction tr1 = new RTtransaction { From = "Alice", To = "Bob", Amount = 100 };
        RTtransaction tr2 = tr1 with { To = "Charlie" }; //여기 with는 뭐지? 지금 간단히 말하면 이 코드는 tr1의 모든 상태를 복사한 다음 To프로퍼티 값만 수정한거다.
        RTtransaction tr3 = tr2 with { From = "Dave", Amount = 30 };

        Console.WriteLine(tr1); //오버라이딩을 통해 출력 형식을 정해놨었다.
        Console.WriteLine(tr2);
        Console.WriteLine(tr3);
    }
}