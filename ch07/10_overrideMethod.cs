using System;

class ArmorSuite
{
    public virtual void Initialize() // virtual 수식어를 붙여 오버라이드 될 준비
    {
        Console.WriteLine("Armored");
    }
}

class IromMan : ArmorSuite //상속된 클래스 
{
    public override void Initialize() //override 수식어가 붙음 
    {
        base.Initialize(); // 본래 기능 
        Console.WriteLine("Repulsor Rays Armed"); //추가된 기능 
    }
}

class WarMachine : ArmorSuite
{
    public override void Initialize()
    {
        base.Initialize();
        Console.WriteLine("Double-Barrel Cannons Armed");
        Console.WriteLine("Micro-Rocket Launcher Armed");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Creating ArmorSuite....");
        ArmorSuite armorSuite = new ArmorSuite();
        armorSuite.Initialize();

        Console.WriteLine("Creating IronMan...");
        ArmorSuite ironman = new IromMan();
        ironman.Initialize();

        Console.WriteLine("\nCreating WarMachine...");
        ArmorSuite warmachine = new WarMachine();
        warmachine.Initialize();
    }
}

