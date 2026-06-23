using System;


class Player
{
    private string PlayerName = "플레이어";
    private int speed = 10;
    private class Inventory // Player 내부에서만 쓸 수 있는 private 클래스 Player 클래스 밖에서는 보이지 않는다.
    {
        public void EquidHeavyArmor(Player owner) //상위 클래스를 매개 변수로 받자 
        {
            Console.WriteLine(owner.PlayerName);  //private 변수이지만 마음대로 접근 가능함 
            Console.WriteLine("무거운 갑옷을 입었다.");

            owner.speed -= 5;
            Console.WriteLine($"이동속도가 {owner.speed}로 감소했다.");
        }
    }

    public void GameStart()
    {
        Inventory myInven = new Inventory(); //내부의 숨겨진 클래스 객체 생성
        myInven.EquidHeavyArmor(this); //자기자신을 넘기기
    }
}

class Program
{
    static void Main()
    {
        Player player = new Player(); //최상위 클래스 객체 생성

        player.GameStart(); //메소드 실행 
    }
}