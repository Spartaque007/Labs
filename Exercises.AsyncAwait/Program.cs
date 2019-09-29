using Exercises.AsyncAwait.Dependences;
using System;
using System.Threading.Tasks;
using Unity;

namespace Exercises.AsyncAwait
{
    internal static class Program
    {
        private static async Task Main()
        {
            var container = new Bootstraper().unityContainer;
            Appliation app = new Appliation(container);
            await app.Run();

            Console.ReadLine();
        
        }
    }
}