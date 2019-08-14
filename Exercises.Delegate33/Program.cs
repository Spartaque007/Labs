using System;

namespace Exercises.Delegate33
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string> a = () => "a";
            Func<string> b = () => "b";
            Func<string> c = () => "c";
            Func<string> d = null;
            var logger = new Logger();

            d += logger.GetWrapper(a);
            d += logger.GetWrapper(b);
            d += logger.GetWrapper(c);

            Console.WriteLine(d());
            Console.ReadLine();
        }
    }
}