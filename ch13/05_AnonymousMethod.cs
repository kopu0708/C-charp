using System;

delegate int Compare(int a, int b);

class Program
{
    static void BubbleSort(int[] DataSet, Compare compare)
    {
        int i = 0;
        int j = 0;
        int temp = 0;

        for(i = 0; i < DataSet.Length - 1; i++)
        {
            for (j = 0; j < DataSet.Length - 1; j++)
            {
                if (compare(DataSet[j], DataSet[j + 1]) > 0)
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
        int[] arr = { 3, 7, 4, 2, 10 };

        Console.WriteLine("Sorting ascending...");
        BubbleSort(arr, delegate (int a, int b) //익명 메소드
        {
            if (a > b)
                return 1;
            else if (a == b)
                return 0;
            else
                return -1;
        });

        for (int i = 0; i < arr.Length; i++)
            Console.Write($"{arr[i]} ");

        int[] arr2 = { 7, 2, 8, 10, 11 };
        Console.WriteLine("\nSorting descending..");
        BubbleSort(arr2, delegate (int a, int b)
        {
            if (a < b)
                return 1;
            else if (a == b)
                return 0;
            else
                return -1;
        });

        for (int i = 0; i < arr2.Length; i++)
            Console.Write($"{arr2[i]} ");

        Console.WriteLine();
    }
}