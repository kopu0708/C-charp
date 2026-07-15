using System;
using System.Collections;
using System.Collections.Generic;

class InventoryFullException : Exception
{
    public int MaxSize { get; }
    public string ItemName { get; }

    public InventoryFullException(string message, int maxSize, string itemName) 
        : base(message) //Exception 클래스의 생성자의 매개변수 message에 정보를 전달 해야한다.
    {
        MaxSize = maxSize;
        ItemName = itemName;
    }
}

class Item //기반이 될 아이템 클래스
{
    public string _Name { get; set; }

    public Item(string _Name)
    {
        this._Name = _Name;
    }
}

class Weapon : Item //무기도 아이템이고 
{
    public int _Damage { get; set; }
    public int _UpgradeLv { get; set; }

    public Weapon(string name, int damage, int UpgradeLv) : base(name)
    {
        _Damage = damage;         
        _UpgradeLv = UpgradeLv;
    }
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
        // 1. 먼저 검사
        if (Count >= _items.Length)
            throw new InventoryFullException(
                "인벤토리가 가득 찼습니다.", _items.Length, item._Name);

        // 2. 통과하면 추가
        _items[Count++] = item;
        Console.WriteLine($"{item._Name} 추가됨");
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
        Weapon sword = new Weapon("검", 50, 0);

        try
        {
            Inventory<Item> smallInv = new Inventory<Item>(2);  // 2칸짜리
            smallInv.Add(sword);
            smallInv.Add(sword);
            smallInv.Add(sword);  // 3번째 → 예외 발생!
        }
        catch (InventoryFullException e)
        {
            Console.WriteLine($"{e.Message} (최대: {e.MaxSize}칸, 실패한 아이템: {e.ItemName})");
        }
    }
}