namespace Exercises.AsyncAwait.Dependences
{
    public interface ILogger
    {
        void Write(string logInformation);

        void WriteRedText<T>(T s);
    }
}