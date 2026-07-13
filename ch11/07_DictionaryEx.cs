using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic["하나"] = "one";
        dic["둘"] = "two";
        dic["셋"] = "three";
        dic["넷"] = "four";
        dic["다섯"] = "five";

        Console.WriteLine(dic["하나"]);
        Console.WriteLine(dic["둘"]);
        Console.WriteLine(dic["셋"]);
        Console.WriteLine(dic["넷"]);
        Console.WriteLine(dic["다섯"]);

    }
}