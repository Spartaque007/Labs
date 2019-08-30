using System;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FileReader fileReader = new FileReader(new ConsoleLogger(), "urls");

            if( await fileReader.GetUrlsFromLocalFileAsync(@"./urls.txt"))
            {
                fileReader.GetDataFromUrlParallel(3);
            }
            await fileReader.SaveAllPagesToFile();
            Console.WriteLine("\nAll done\npress ENTER for exit");
            Console.ReadLine();
        }
    }
}