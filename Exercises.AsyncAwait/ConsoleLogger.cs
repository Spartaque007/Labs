using Exercises.AsyncAwait.Dependences;
using System;

namespace Exercises.AsyncAwait
{
    class ConsoleLogger : ILogger
    {
        public void Write(string logInformation)
        {
            Console.WriteLine(logInformation);
        }
    }
}