using System;


delegate int Compare<T>(T a, T b);

class Program
{
   static int AscendCompare<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b);
    }

    static int DescendCompare<T>(T a, T b) where T : IComparable<T>
    { 
        return a.CompareTo(b) * -1; //-1 곱하면 자신보다 큰 경우 -1 반환, 자신보다 작은 경우 1 반환
    }

    static void BubbleSort<T>(T[] dataSet, Compare<T> compare)
    {
        T temp;
        for (int i = 0; i < dataSet.Length - 1; i++)
        {
            for (int j = 0; j < dataSet.Length - 1 - i; j++)
            {
                if (compare(dataSet[j], dataSet[j + 1]) > 0)
                {
                    temp = dataSet[j + 1];
                    dataSet[j + 1] = dataSet[j];
                    dataSet[j] = temp;
                }
            }
        }
    }

    static void Main()
    {
        int[] intArray = { 3, 7, 4, 2, 1, 5, 6 };
        Compare<int> intCompare = new Compare<int>(AscendCompare);
        BubbleSort(intArray, intCompare);
        foreach (int i in intArray)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        string[] stringArray = { "Cherry", "Apple", "Blueberry" };
        Compare<string> stringCompare = new Compare<string>(DescendCompare);
        BubbleSort(stringArray, stringCompare);
        foreach (string s in stringArray)
        {
            Console.Write(s + " ");
        }
    }
}
