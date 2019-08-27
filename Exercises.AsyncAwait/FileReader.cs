using Exercises.AsyncAwait.Dependences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    public sealed class FileReader
    {
        private const string DefaultDir = @"./urls/";

        private readonly string UrlsFileName;

        private readonly ILogger _logger;


        public List<string> Urls { get; set; } = new List<string>();


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
                    _logger.Write("started fetching <urls>");

                    foreach (string url in File.ReadLines(UrlsFileName))
                    {
                        Urls.Add(url);
                    }
                    _logger.Write("fetched successfully");
                    return true;
                }
                catch (FileNotFoundException)
                {
                    _logger.Write("File not found");
                }
                return false;
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
                    try
                    {
                        var currentFileName = Regex.Replace(url, @"[^\w\.@-]", "");
                        var currentPath = $@"{DefaultDir}{currentFileName}.txt";
                        using (StreamWriter sw = new StreamWriter(currentPath))
                        {
                            sw.WriteLine(url);
                            _logger.Write($"File \"{currentFileName}\" saved");
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Write($"Url don't saved because: \n{e.Message}");
                    }
                }
            });
        }
    }
}