using System;

namespace Exercises.Delegate33
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string> functions = null;
            functions += () => "1";
            functions += () => "2";
            functions += () => "3";
            var result = functions.InvokeList();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}