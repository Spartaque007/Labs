using System;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    internal static class Program
    {
        private static async Task Main()
        {
            var a = 0;
            while (a != 110)
            {
                Console.Clear();
                ConsoleWithLocker.Clear();
                await Foo();
                Console.WriteLine("Press N for exit or any button to repeat downloads and press ENTER");
                a = ConsoleWithLocker.Read();
                Console.WriteLine(a);
            }
        }

        private static async Task Foo()
        {
            var logger = new ConsoleLogger();
            var storage = new LocalFileStorage(logger, @".\urls\", "urls");
            var urlSaver = new UrlSaver(storage, logger);
            var statusLine = new AnimatedConsoleStatusReporter("Loading progress: ");

            urlSaver.UpdateStatusLine += ((s, e) =>
            {
                statusLine.Update(e.Delta);
            });

            if (await urlSaver.TryGetGetUrlsFromStorageAsync())
            {
                await urlSaver.GetDataFromUrlAsync(5);
                await urlSaver.SaveContentToStorageAsync();
                ConsoleWithLocker.WriteLine("************** ALL DONE**************");
            }
        }
    }
}