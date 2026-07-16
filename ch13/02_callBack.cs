using System;

delegate int Compare(int a, int b);

class Program
{
    static int AscendCompare(int a, int b)
    {
        if (a > b)
            return 1;
        else if (a == b)
            return 0;
        else
            return -1;
    }

    static int DescendCompare(int a, int b)
    {
        if (a < b)
            return 1;
        else if (a == b)
            return 0;
        else
            return -1;
    }

    static void BubbleSort(int[] DataSet, Compare comparer)
    {
        int i = 0;
        int j = 0;
        int temp = 0;

        for (i = 0; i < DataSet.Length - 1; i++)
        {
            for (j = 0; j < DataSet.Length - (i + 1); j++)
            {
                if (comparer(DataSet[j], DataSet[j + 1]) > 0)
                {
                    temp = DataSet[j + 1];
                    DataSet[j + 1] = DataSet[j];
                    DataSet[j] = temp;
                }
            }
        }
    }

    static void Main()
    {
        int[] array = { 3, 7, 4, 2, 10 };

        Console.WriteLine("Sorting ascending...");
        BubbleSort(array, AscendCompare); // 같은 클래스 안이라 메서드 이름만 써도 됨
                                          // new Compare(AscendCompare) 로 써도 되지만 컴파일러가 알아서 변환해줌
        foreach (int n in array)
            Console.Write($"{n} ");

        Console.WriteLine();

        Console.WriteLine("Sorting descending...");
        BubbleSort(array, DescendCompare);
        foreach (int n in array)
            Console.Write($"{n} ");
    }
}
