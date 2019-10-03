using Exercises.AsyncAwait.Dependences;
using Unity;
using Unity.Injection;

namespace Exercises.AsyncAwait
{
    public class Bootstrapper
    {
        public UnityContainer unityContainer;

        public Bootstrapper()
        {
            unityContainer = new UnityContainer();
            unityContainer.RegisterSingleton<IUrlSaver, UrlSaver>();
            unityContainer.RegisterSingleton<ILogger, ConsoleLogger>();
            unityContainer.RegisterInstance<IStorage>(new LocalFileStorage(unityContainer.Resolve<ILogger>(), @".\urls\", "urls"), InstanceLifetime.Singleton);
            unityContainer.RegisterInstance<IStatusReporter>(new AnimatedConsoleStatusReporter("Loading :"), InstanceLifetime.Singleton);
        }
    }
}