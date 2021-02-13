using System;
using BassetS.Library.Core;

namespace BassetS.Test.DevTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("USERPROFILE"));
            Console.WriteLine(Environment.GetEnvironmentVariable("BASSETSCONFIG"));
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
