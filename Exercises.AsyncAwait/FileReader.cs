using Exercises.AsyncAwait.Dependences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    public sealed class FileReader
    {
        private const string DefaultDir = @"./urls/";

        private readonly string _urlsFileName;

        private readonly ILogger _logger;

        private readonly IStatus _statusLine;

        private Dictionary<string, string> results = new Dictionary<string, string>();


        public IList<string> Urls { get; set; } = new List<string>();


        public FileReader(ILogger logger, IStatus statusLine, string urlsFileName)
        {
            _logger = logger;
            _urlsFileName = $"./{urlsFileName}.txt";
            _statusLine = statusLine;
        }


        public async Task<bool> GetUrlsFromLocalFileAsync(string pathToFileWithUrls)
        {
            _logger.Write("Start reading file");

            return await Task.Run(() =>
            {
                try
                {
                    _logger.Write($"started getting URLs from file {_urlsFileName}");

                    foreach (string url in File.ReadLines(_urlsFileName))
                    {
                        results[url] = null;
                    }
                    _logger.Write("Getting URls from file successfully");

                    return true;
                }
                catch (FileNotFoundException)
                {
                    _logger.Write("File not found");
                }
                catch (ArgumentException)
                {
                    _logger.Write("Path is a zero-length string");
                }
                catch (SecurityException)
                {
                    _logger.Write("You don't have the required permission");
                }

                return false;
            });
        }

        public async void GetDataFromUrlOneByOneAsync()
        {
            decimal quantumPercent = (decimal)100/results.Count;
            for (int i = 0; i < results.Count; i++)
            {
                var currentKeyValuePair = results.ElementAt(i);
                results[currentKeyValuePair.Key] = await GetStringFromUrlAsync(currentKeyValuePair.Key);
                _statusLine.Update(quantumPercent);
            }
        }

        public async void GetDataFromUrlParallelAsync(int countDownloads)
        {
            decimal quantumPercent = (decimal)100 / results.Count;
            if (countDownloads <= 0)
            {
                throw new ArgumentException("СountDownloads must be greater than zero");
            }
            else
            {
                var queueOfDownloads = new Queue<KeyValuePair<string, string>>(results);

                while (queueOfDownloads.Count != 0)
                {
                    int countOfTasks = 0;
                    if (queueOfDownloads.Count >= countDownloads)
                    {
                        countOfTasks = countDownloads;
                    }
                    else
                    {
                        countOfTasks = queueOfDownloads.Count;
                    }
                    var tasksArray = new Task<string>[countOfTasks];
                    var urlsArray = new string[countOfTasks];

                    for (int i = 0; i < countOfTasks; i++)
                    {
                        var currentDownloadUrl = (queueOfDownloads.Dequeue()).Key;
                        urlsArray[i] = currentDownloadUrl;
                        tasksArray[i] = GetContentFromUrlAsync(currentDownloadUrl);
                    }

                    Task.WaitAll(tasksArray);
                    for (int i = 0; i < countOfTasks; i++)
                    {
                        results[urlsArray[i]] = await tasksArray[i];
                        _statusLine.Update(quantumPercent);
                    }
                }
            }
        }

        public async Task SaveAllPagesToFile()
        {
            foreach (var result in results)
            {
                await SaveContentToFileAsync(result.Key, result.Value);
            }
        }


        private async Task SaveContentToFileAsync(string url, string content)
        {
            await Task.Run(() =>
            {
                var directory = new DirectoryInfo(DefaultDir);

                if (!directory.Exists)
                {
                    try
                    {
                        directory.Create();
                    }
                    catch (IOException)
                    {
                        _logger.Write("Directory can't created");
                    }

                    _logger.Write("Directory was created");
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
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
            return (client).GetStringAsync(url);
        }

        private async Task<string> GetContentFromUrlAsync(string url)
        {
            try
            {
                return await GetStringFromUrlAsync(url);
            }
            catch (HttpRequestException ex)
            {
                _logger.Write($"Request filed {ex.TargetSite.Name}");
            }
            return null;
        }
    }
}