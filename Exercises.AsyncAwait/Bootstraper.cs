using Exercises.AsyncAwait.Dependences;
using Unity;

namespace Exercises.AsyncAwait
{
    public class Bootstraper
    {
        public UnityContainer unityContainer;

        public Bootstraper()
        {
            unityContainer = new UnityContainer();
            unityContainer.RegisterType<IUrlSaver, >();
            unityContainer.RegisterSingleton<ILogger, ConsoleLogger>();
            unityContainer.RegisterSingleton<IStorage, LocalFileStorage>();
            
            unityContainer.RegisterSingleton<IStatusReporter, AnimatedConsoleStatusReporter>("Loading progress: ");
        }
    }
}