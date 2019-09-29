using Exercises.AsyncAwait.Dependences;

namespace Exercises.AsyncAwait
{
    class ConsoleLogger : ILogger
    {
        public void Write(string logInformation)
        {
            ConsoleWithLocker.WriteLine(logInformation);
        }

        public void WriteRedText<T>(T s)
        {
            ConsoleWithLocker.WriteException(s);
        }
    }
}