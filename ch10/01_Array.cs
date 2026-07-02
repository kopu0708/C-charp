using System;

class Program
{
    static void Main()
    {
        int[] scores = new int[5]; //0부터 4까지의 번호가 붙은 5개 그냥 인덱스가 0부터 시작한다

        scores[0] = 30;
        scores[1] = 40;
        scores[2] = 81;
        scores[3] = 92;
        scores[4] = 74; //배열의 마지막 

        foreach(int score in scores) //배열 전체를 순회한다.
        {
            Console.WriteLine(score);
        }
        int sum = 0;

        foreach(int score in scores)
        {
            sum += score;
        }

        int avg = sum / scores.Length;  

        Console.WriteLine($"평균 : {avg}");

    }
}