using System;

record Item //아이템 강화 시스템을 만들어보자
{
    public string ItemName { get; init; } //아이템 이름은 변하지 않을 것이다.
    public int Damage { get; init; } //아이템 데미지
    public int ItemLv { get; init; } //몇강인지도 기록해두자 
}

class Program
{
    static void Main()
    {
        Item sword = new Item { ItemName = "강철검", Damage = 10, ItemLv = 0 }; //초기 아이템 생성 
        Console.WriteLine($"강화할 아이템 : {sword.ItemName}, 강화 단계 : {sword.ItemLv}");

        Item upgradeSword = UpgradeItem(sword);

        if (sword.Equals(upgradeSword)) // 이것만 추가했다. ==을 써도 Equals를 호출하도록 만들어져 있다고 한다.
        {
            Console.WriteLine("바뀐게 없네요");
        }
        else
        {
            Console.WriteLine(
                $"강화 후 공격력 : {sword.Damage} + {upgradeSword.Damage - sword.Damage}, 강화 단계 : {sword.ItemLv} -> {upgradeSword.ItemLv}");
        }
    }


    static Item UpgradeItem(Item item) //온라인 게임의 아이템 강화의 묘미는 랜덤성이다.
    {
        Random random = new Random();
        int success = random.Next(1, 101);

        if (success >= 50) //결과를 확인해야하니 적당이 반반으로 가자
        {
            Console.WriteLine("강화 성공!");
            return item with { Damage = item.Damage + random.Next(1,21), ItemLv = item.ItemLv + 1 }; //여기서 with 연사자를 통해 새로운 객체를 생성하면서 원본은 유지하고 특정 값만 수정한다.
        }                                                                                            // 공격력이 오르는 것도 랜덤으로 만들어줬다.
        else
        {
            Console.WriteLine("강화 실패 ㅅㄱ");
            return item;
        }
    }
}