using System;


class SaveData
{
    private int _playTime = 0;
    private DateTime _saveTime;

    public void Save(int playTime)
    {
        try
        {
            Console.WriteLine("세이브 파일 여는 중...");

            if (playTime < 0)
                throw new ArgumentException("플레이 시간이 음수입니다!");

            _playTime = playTime;
            _saveTime = DateTime.Now;
            Console.WriteLine($"저장 완료: {_playTime}초, {_saveTime}");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"저장 실패: {e.Message}");
        }
        finally
        {
            Console.WriteLine("세이브 파일 닫기");  // 성공하든 실패하든 무조건
        }
    }
}
class Programpu
{
    static void Main()
    {
        SaveData save = new SaveData();

        save.Save(3600);   // 정상 저장
        Console.WriteLine("---");
        save.Save(-100);   // 예외 발생 → 그래도 파일은 닫힘
    }
}