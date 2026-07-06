using System;
using System.Collections;

class MyList : IEnumerable, IEnumerator
{
    private int[] array;
    int position = -1; //컬렉션의 현재 위치를 다루는 변수이다. 0은 배열의 첫번째 요소를 가리키는 수이다. 
                       //position이 0을 갖고 있을 때 foreach문이 첫 번째 반복을 수행하면 MoveNext() 메소드를 실행하고,
                       // 이때 position이 1이 되어 두번째 요소를 가져오게 된다. 즉 -1은 시작전을 의미 

    public MyList()
    {
        array = new int[3];
    }
    public int this[int index]
    {
        get{ return array[index]; }

        set 
        {
            if (index >= array.Length) 
            {
                Array.Resize<int>(ref array, index + 1);
                Console.WriteLine($"Array Resized : {array.Length}");
            }

            array[index] = value;
        }
    }
    
    //IEnumerator 멤버
    public object Current
    {
        get { return array[position]; }

    }

    //IEnumerator 멤버
    public bool MoveNext()
    {
        if(position == array.Length - 1)
        {
            Reset();
            return false;
        }

        position++;
        return (position < array.Length);
    }

    public void Reset()
    {
        position = -1;
    }

    //IEnumerator 멤버
    public IEnumerator GetEnumerator()
    {
        return this;
    }

}

class MainApp
{
    static void Main(string[] args)
    {
        MyList list = new MyList();
        for(int i = 0; i<5; i++)
        {
            list[i] = i;
        }

        foreach (int e in list)
        {
            Console.WriteLine(e);
        }
    }
}