using System;
using System.Collections;


enum Item { None = 0, Sword = 101, Shield = 202 }
class Inventory //이번에는 게임 인밴을 구현한다고 가정해보자 
{
    private int[] _Inventory; //인벤토리는 안전하게 저장되어야한다. 

    public Inventory()
    {
        _Inventory = new int[5];
        _Inventory[0] = (int)Item.Sword;   // 시작 아이템
        _Inventory[1] = (int)Item.Shield;
    }

    public int this[int index] 
    {
        get
        {
            return _Inventory[index];  //무슨 아이템인지 인덱스로 받아서 반환 
        }

        set
        {
            if (index >= _Inventory.Length)
                Array.Resize <int > (ref _Inventory, index + 1);
  
            _Inventory[index] = value; //인덱스를 통해 접근해 인벤토리에 저장 
        }
    }
}

class Program
{
    static void Main()
    {
        Inventory inv = new Inventory();
        inv[0] = 101;
        inv[5] = 202;  // 범위 초과 → 리사이즈
        Console.WriteLine((Item)inv[0]);
        Console.WriteLine((Item)inv[1]);
        Console.WriteLine((Item)inv[5]);
    }
}