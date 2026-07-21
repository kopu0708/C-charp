using System;

class Program
{
    delegate string Concatenate(string[] args);

    static void Main(string[] args)
    {
        string array =  Console.ReadLine();
        string[] words = array.Split(' ');
            
        Concatenate concat = 
            (arr) =>
            {
                string result = "";
                foreach (string s in arr)
                    result += s;

                return result;
            };

        Console.WriteLine(concat(words));
    }
}