using System;
using System.Threading.Tasks;
using Unity;

namespace Exercises.AsyncAwait
{
    internal static class Program
    {
        private static async Task Main()
        {
            var container = new Bootstrapper().unityContainer;
            var app = container.Resolve<Application>();
            await app.Run();
            Console.ReadLine();
        }
    }
}