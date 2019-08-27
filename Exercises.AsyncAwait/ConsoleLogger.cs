using Exercises.AsyncAwait.Dependences;

namespace Exercises.AsyncAwait
{
    class ConsoleLogger : ILogger
    {
        public void Write(string logInformation)
        {

            System.Console.WriteLine(logInformation);
        }
    }
}