using System;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FileReader fileReader = new FileReader(new ConsoleLogger(), "urls");

            if( await fileReader.GetDataAsync(@"./urls.txt"))
            {
                fileReader.GetDataFromUrlOneByOneAsync();
            }
            await fileReader.GetDataFromUrlOneByOneAsync();
            Console.WriteLine("\nAll done\npress ENTER for exit");

            
            Console.ReadKey();
        }
    }
}