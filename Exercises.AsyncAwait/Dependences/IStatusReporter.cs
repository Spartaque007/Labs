namespace Exercises.AsyncAwait.Dependences
{
    public interface IStatusReporter
    {
        int CurrentStatus { get; }


        void Update(decimal count);
    }
}