using System;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FileReader fileReader = new FileReader(new ConsoleLogger(), new ConsoleLoadingAnimations("Download status"), "urls");

            if (await fileReader.GetUrlsFromLocalFileAsync(@"./urls.txt"))
            {

                fileReader.GetDataFromUrlParallelAsync(3);
            }
            await fileReader.SaveAllPagesToFile();
            Console.WriteLine("\nAll done\npress ENTER for exit");
            Console.ReadLine();


            Console.ReadKey();


        }
    }
}