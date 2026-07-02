using System;

class MainApp
{
    private static bool CheckPassed(int score)
    {
        return score >= 60;
    }

    private static void Print(int Value)
    {
        Console.WriteLine($"{Value}");
    }

    static void Main()
    {
        int[] scores = new int[] { 80, 74, 81, 90, 34 };

        foreach (int score in scores)
        {
            Console.Write($"{score} ");
        }
        Console.WriteLine();

        Array.Sort(scores); // 이친구는 배열을 정렬해준다.

        Array.ForEach<int>(scores, new Action<int>(Print)); //ForEach는 배열의 모든 요소에 같은 작업을 수행하게 한다. 
                                                            //Action은 대리자라는 건데 추후 나중에 자세히 나온다.
        
        Console.WriteLine();

        Console.WriteLine($"Number of dimensions : {scores.Rank}");

        Console.WriteLine($"Binary Search : 81 is at" + $"{Array.BinarySearch<int>(scores, 81)}"); //이진 탐색을 실행한다.

        Console.WriteLine($"Binary Search : 90 is at" + $"{Array.IndexOf<int>(scores, 90)}"); // 찾고자하는 특정 데이터의 인덱스를 반환한다.

        Console.WriteLine($"Binary Search ? : " + $"{Array.TrueForAll<int>(scores, CheckPassed)}"); // TrueForAll은 배열과 함꼐 조건을 검사하는 메소드를 매개변수로 받는다.

        int index = Array.FindIndex<int>(scores, (score) => score < 60); //FindIndex는 메소드를 특정 조건에 부합하는 메소드를 매개변수로 받는다. 여기선 람다식으로 구현

        scores[index] = 61;
        Console.WriteLine($"Everyone passed ? : " + $"{Array.TrueForAll<int>(scores, CheckPassed)}");

        Console.WriteLine("Old length of scores : " + $"{scores.GetLength(0)}");

        Array.Resize<int>(ref scores, 10); //5였던 배열의 용량을 10으로 재조정

        Array.ForEach<int>(scores, new Action<int>(Print));
        Console.WriteLine();

        Array.Clear(scores, 3, 7);
        Array.ForEach<int>(scores, new Action<int>(Print));
        Console.WriteLine();

        int[] sliced = new int[3];
        Array.Copy(scores, 0, sliced, 0, 3); //scores배열의 0번째부터 3개의 요소를 sliced 배열의 0번쨰 ~ 2번째 요소에 차례대로 복사한다.
        Array.ForEach<int>(sliced, new Action<int>(Print));
        Console.WriteLine();
    }
}