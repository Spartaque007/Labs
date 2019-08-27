using Exercises.AsyncAwait.Dependences;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    public sealed class FileReader
    {
        private readonly ILogger _logger;


        public List<string> Urls { get; set; } = new List<string>();


        public FileReader(ILogger logger)
        {
            _logger = logger;
        }


        public async Task GetDataAsync(string pathToFileWithUrls)
        {
            _logger.Write("Start reading file");
            await  Task.Run(() =>
            {
                _logger.Write("started fetching <urls>");

                foreach (string url in File.ReadLines(pathToFileWithUrls))
                {
                    Urls.Add(url);
                }
                _logger.Write("fetched successfully");
            });
        }


        public async Task SaveUrlsToFileAsync ()
        {
            DirectoryInfo directory = new DirectoryInfo(@"./urls");
            if(!directory.Exists)
            {
                directory.Create();
            }
        }
    }
}