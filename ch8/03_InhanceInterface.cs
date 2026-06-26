using System;

interface IAttackable
{
    void Attack();  //공격 메소드를 꼭 만드세요
}
//공격만 있던 인터페이스 기능을 추가하고 싶어짐 근데 밑에 이미 해당 인터페이스를 사용하는 클래스들이 다수 존재한다.

interface IPowerAttackable : IAttackable  //상속은 다 이런 형태 강공격을 추가해보자 
{
    void PowerAttack(); //당연하지만 상속은 기반 인터페이스의 모든 것을 가져오기에 추가할 기능만 선언 
}

class Warrior : IAttackable  //인터페이스 상속 
{
    public void Attack()
    {
        Console.WriteLine("도끼로 머리 찍기"); //실제론 더 복잡한 코드가 들어가겠다.
    }
}

class Mage : IAttackable  //똑같은 인터페이스를 상속 받았다.
{
    public void Attack()
    {
        Console.WriteLine("원거리 마법으로 머리 뚫기");  //같은 인터페이스를 상속 받았지만  메소드 구현부는 다르게 구현
    }
}

class SecondWarrior : IPowerAttackable //전사가 2차 전직해서 강공격도 가능해졌다. 
{
    public void Attack()
    {
        Console.WriteLine("도끼로 머리 찍기");
    }
    public void PowerAttack()
    {
        Console.WriteLine("도끼로 머리 더 쌔게 찍기");
    }
}

class Program
{
    static void Main()
    {
        // 두 클래스 모두 IAttackable이라는 같은 약속을 따름
        IAttackable player1 = new Warrior();
        IAttackable player2 = new Mage();

        // 새로운 강공격 전사는 IPowerAttackable 타입으로 담을 수 있다.
        IPowerAttackable player3 = new SecondWarrior();

        player1.Attack();
        player2.Attack();

        player3.Attack();       // 부모 인터페이스의 기능도 당연히 쓸 수 있고
        player3.PowerAttack();  // 자신이 가진 확장 기능도 쓸 수 있다.
    }
}