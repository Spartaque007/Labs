using Exercises.AsyncAwait.Dependences;
using Unity;
using Unity.Injection;

namespace Exercises.AsyncAwait
{
    public class Bootstrapper
    {
        public IUnityContainer UnityContainer { get; }

        public Bootstrapper()
        {
            UnityContainer = new UnityContainer();
            UnityContainer.RegisterSingleton<IUrlSaver, UrlSaver>();
            UnityContainer.RegisterSingleton<ILogger, ConsoleLogger>();
            UnityContainer.RegisterInstance<IStorage>(new LocalFileStorage(UnityContainer.Resolve<ILogger>(), @".\urls\", "urls"), InstanceLifetime.Singleton);
            UnityContainer.RegisterInstance<IStatusReporter>(new AnimatedConsoleStatusReporter("Loading :"), InstanceLifetime.Singleton);
        }
    }
}