using System;

interface IAttackable
{
    void Attack(); //공격 메소드를 꼭 만드세요 
}

class Warrior : IAttackable //인터페이스 상속 
{
    public void Attack()
    {
        Console.WriteLine("도끼로 머리 찍기"); //실제론 더 복잡한 코드가 들어가겠다.
    }
}

class Mage : IAttackable //똑같은 인터페이스를 상속 받았다.
{
    public void Attack()
    {
        Console.WriteLine("원거리 마법으로 머리 뚫기");  //같은 인터페이스를 상속 받았지만  메소드 구현부는 다르게 구현
    }
}

class Program
{
    static void Main()
    {
        // 두 클래스 모두 IAttackable이라는 같은 약속을 따름
        IAttackable player1 = new Warrior();
        IAttackable player2 = new Mage();

        player1.Attack();
        player2.Attack();
    }
}