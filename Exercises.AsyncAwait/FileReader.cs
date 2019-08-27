using Exercises.AsyncAwait.Dependences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Exercises.AsyncAwait
{
    public sealed class FileReader
    {
        private const string DefaultDir = @"./urls";
        private readonly ILogger _logger;


        public List<string> Urls { get; set; } = new List<string>();


        public FileReader(ILogger logger)
        {
            _logger = logger;
        }


        public async Task GetDataAsync(string pathToFileWithUrls)
        {
            _logger.Write("Start reading file");
            await Task.Run(() =>
           {
               _logger.Write("started fetching <urls>");

               foreach (string url in File.ReadLines(pathToFileWithUrls))
               {
                   Urls.Add(url);
               }
               _logger.Write("fetched successfully");
           });
        }


        public async Task SaveUrlsToFileAsync()
        {
            await Task.Run(() =>
            {
                DirectoryInfo directory = new DirectoryInfo(DefaultDir);
                if (!directory.Exists)
                {
                    _logger.Write("Directory was created");
                    directory.Create();
                }
                foreach (var url in Urls)
                {
                    var currentFileName = Regex.Replace(url, @"[^\w\.@-]", "");
                    using (StreamWriter sw = new StreamWriter($@"{DefaultDir}{currentFileName}.txt"))
                    {
                        sw.WriteLine(url);
                        _logger.Write($"File \"{currentFileName}\" saved");
                    }
                }
            });
        }
    }
}