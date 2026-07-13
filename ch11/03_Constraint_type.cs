using System;
class Item //기반이 될 아이템 클래스
{
    public string _Name { get; set; }
}

class Weapon : Item //무기도 아이템이고 
{
    public int _Damage { get; set; }
    public int _UpgradeLv { get; set; }
}

class Potion : Item //포션도 아이템이다.
{
    public int _count { get; set; }
}

class Inventory<T> where T : Item //아이템만 담을 수 있는 인벤토리 
{
    private T[] _items;
    public int Count { get; private set; }

    public Inventory(int size)
    {
        _items = new T[size]; 
    }

    public void Add(T item)
    {
        _items[Count++] = item;
        Console.WriteLine($"{item._Name} 추가됨");
    }

    public static void Enhance<W>(W item) where W : Weapon
    {
        item._Damage += 10;
        item._UpgradeLv++;
        Console.WriteLine($"{item._Name} 강화! 공격력: {item._Damage}, 강화 단계: {item._UpgradeLv}");
    }

    public static U CreateItem<U>() where U : Item, new() //기본 생성자가 있는 item 파생 형식만 생성 가능 
    {
        return new U();
    }

    public T this[int index]
    {
        get { return _items[index]; }
    }
}
class Program 
{
    static void Main()
    {
        //무기 인벤토리 
        Inventory<Weapon> weaponInv = new Inventory<Weapon>(5);

        Weapon sword = Inventory<Weapon>.CreateItem<Weapon>();
        sword._Name = "검";
        sword._Damage = 50;

        weaponInv.Add(sword);
        Inventory<Weapon>.Enhance(sword);

        // 포션 인벤토리
        Inventory<Potion> potionInv = new Inventory<Potion>(5);

        Potion hp = Inventory<Potion>.CreateItem<Potion>();
        hp._Name = "HP 포션";
        hp._count = 5;

        potionInv.Add(hp);

        Inventory<Item> inventory = new Inventory<Item>(10); //이렇게 하면 아이템이면 다 담을 수 있다.
        inventory.Add(hp);
        inventory.Add(sword);

        for (int i = 0; i < inventory.Count; i++)
        {
            Console.WriteLine(inventory[i]._Name);
        }

        // 이건 컴파일 에러 — Item을 상속받지 않은 형식은 불가
        // Inventory<int> intInv = new Inventory<int>();
    }
}

