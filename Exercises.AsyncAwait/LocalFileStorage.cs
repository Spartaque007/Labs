﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Exercises.AsyncAwait.Dependences;

namespace Exercises.AsyncAwait
{
    class LocalFileStorage : IStorage
    {
        private readonly string _defaultDir;

        private readonly string _urlsFileName;

        private readonly ILogger _logger;

        private IDictionary<string, string> _results;



        public LocalFileStorage(ILogger logger, string defaultDir, string urlsFileName)
        {
            _defaultDir = defaultDir;
            _logger = logger;
            _urlsFileName = $"./{urlsFileName}.txt";
            _results = new Dictionary<string, string>();
        }


        public async Task<IDictionary<string, string>> GetUrlsFromStorageAsync()
        {
            string textFromFile;
            var urlsDictionary = new Dictionary<string, string>();

            using (StreamReader reader = File.OpenText(_urlsFileName))
            {
                textFromFile = await reader.ReadToEndAsync();
            }

            var urlsArray = textFromFile.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var url in urlsArray)
            {
                urlsDictionary[url] = null;
            }

            return urlsDictionary;
        }


        public async Task SaveContentToStorage(string url, string content)
        {
            await Task.Run(() =>
            {
                var directory = new DirectoryInfo(_defaultDir);

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
                    var currentPath = $@"{_defaultDir}{currentFileName}.html";
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
    }
}