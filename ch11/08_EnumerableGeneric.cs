using System;
using System.Collections;
using System.Collections.Generic;
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

class Inventory<T> : IEnumerable<T>  where T : Item //t상속을 추가 했다.
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

    public T this[int index] //순회용 메서드 인덱스 번호를 받아 내부 값을 꺼내옴 
    {
        get { return _items[index]; }
    }

    public IEnumerator<T> GetEnumerator() //일반화 버전을 구현 
    {
        for(int i = 0; i < _items.Length; i++)
        {
            yield return _items[i]; //앞서 배웠듯이 yield를 쓰면 IEnumerator<T>의 메소드 프로퍼티를 구현할 필요가 없다.
        }
    }

    IEnumerator IEnumerable.GetEnumerator()  // 두가지 버전다 구현해야 하므로 메소드를 재활용해 만들자
    {
        return GetEnumerator();
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

        /* for (int i = 0; i < inventory.Count; i++)
         {
             Console.WriteLine(inventory[i]._Name);
         } */ //보다시피 foreach를 사용하지 않고 인덱스 번호를 이용해 돌았었다.

        foreach (Item item in inventory) //이제 내가 만든 일반화 클래스도 foreach가 가능하다.
        {
            Console.WriteLine(item._Name);
        }
    }
}
