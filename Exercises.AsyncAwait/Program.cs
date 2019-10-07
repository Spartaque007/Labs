using System;
using System.Threading.Tasks;
using Unity;

namespace Exercises.AsyncAwait
{
    internal static class Program
    {
        private static async Task Main()
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.UnityContainer;
            var app = container.Resolve<Application>();
            await app.Run();
            Console.ReadLine();
        }
    }
}