using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FileReader fileReader = new FileReader(new ConsoleLogger());
            await fileReader.GetDataAsync(@"./urls.txt");
        }
    }
}