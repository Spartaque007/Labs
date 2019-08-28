using Exercises.AsyncAwait.Dependences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    public sealed class FileReader
    {
        private const string DefaultDir = @"./urls/";

        private readonly string UrlsFileName;

        private readonly ILogger _logger;


        public IList<string> Urls { get; set; } = new List<string>();


        public FileReader(ILogger logger, string urlsFileName)
        {
            _logger = logger;
            UrlsFileName = $"./{urlsFileName}.txt";
        }


        public async Task<bool> GetDataAsync(string pathToFileWithUrls)
        {
            _logger.Write("Start reading file");
            return await Task.Run(() =>
            {
                try
                {
                    _logger.Write($"started getting URLs from file {UrlsFileName}");

                    foreach (string url in File.ReadLines(UrlsFileName))
                    {
                        Urls.Add(url);
                    }
                    _logger.Write("Getting URls from file successfully");
                    return true;
                }
                catch (FileNotFoundException)
                {
                    _logger.Write("File not found");
                }
                return false;
            });
        }


        public async Task GetDataFromUrlOneByOneAsync()
        {
            var urlQueue = new Queue<string>(Urls);

            while (urlQueue.Count != 0)
            {
                try
                {
                    string currentUrl = urlQueue.Dequeue();
                    var result = await GetStringFromUrlAsync(currentUrl);
                    await SaveContentToFileAsync(currentUrl, result);
                }
                catch (HttpRequestException ex)
                {
                    _logger.Write($"Request filed {ex.TargetSite.Name}");
                }
            }

        }

        public async Task SaveContentToFileAsync(string url, string content)
        {

            await Task.Run(() =>
            {
                DirectoryInfo directory = new DirectoryInfo(DefaultDir);

                if (!directory.Exists)
                {
                    _logger.Write("Directory was created");
                    directory.Create();
                }


                try
                {
                    var currentFileName = Regex.Replace(url, @"[^\w\.@-]", "_");
                    var currentPath = $@"{DefaultDir}{currentFileName}.html";
                    using (StreamWriter sw = new StreamWriter(currentPath))
                    {
                        sw.WriteLine(content);
                        _logger.Write($"File \"{url}\" saved");
                    }
                }
                catch (Exception e)
                {
                    _logger.Write($"URL don't saved because: \n{e.Message}");
                }
            });
        }


        private Task<string> GetStringFromUrlAsync(string url)
        {
            return (new HttpClient()).GetStringAsync(url);
        }
    }
}