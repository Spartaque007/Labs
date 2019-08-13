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
            var invoker = new Invoker(functions);
            var result = invoker.GetResultFromFunctions();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}