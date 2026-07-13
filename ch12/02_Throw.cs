using System;

class Program
{
    private int _hp;
    public int Hp
    {
        get { return _hp; }
        set
        {
            if (value < 0)
                throw new Exception("HP는 음수가 될 수 없습니다."); //여기서 던저진 예외는 호출하는 try-catch문에서 받아진다.
            _hp = value;
        }
    }
    static void Main()
    {
        Program p = new Program();
        try
        {
            p.Hp = -30; // 여기서 예외 발생 → catch로 점프
            Console.WriteLine("HP 설정 완료");  // 실행 안 됨
        }

        catch (Exception e)
        {
            Console.WriteLine($"예외 발생: {e.Message}");
        }
    }
}