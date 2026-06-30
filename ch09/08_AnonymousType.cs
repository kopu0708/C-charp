using System;

class Program
{
    static void Main()
    {
        var a = new { Name = "박상현", Age = 123 }; // 이게 무명 형식 
        Console.WriteLine($"Name:{a.Name}, Age:{a.Age}");

        var b = new { Subject = "수학", Scores = new int[] { 90, 80, 70, 60 } };

        Console.WriteLine($"Subject:{b.Subject}, Scores: ");
        foreach (var score in b.Scores)
            Console.Write($"{score} ");

        Console.WriteLine();
    }
}