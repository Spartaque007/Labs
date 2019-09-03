using Exercises.AsyncAwait.Dependences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    public sealed class UrlSaver
    {
        private readonly IStorage _storage;
        private readonly ILogger _logger;

        private IDictionary<string, string> _urls;


        public UrlSaver(IStorage storage, ILogger logger)
        {
            _storage = storage;
            _logger = logger;
            _urls = new Dictionary<string, string>();
        }


        public async Task<bool> TryGetGetUrlsFromStorageAsync()
        {
            _urls = await _storage.GetUrlsFromStorageAsync();
            return _urls.Count > 0;
        }

        public void GetDataFromUrlAsync(int downloadsQuantity)
        {
            var queue = new Queue<string>();
            foreach (var url in _urls)
            {
                queue.Enqueue(url.Key);
            }
            var tasksInTimeQuantity = downloadsQuantity >= _urls.Count
                ? _urls.Count
                : downloadsQuantity;

            var tasksCompleted = 0;

            while (tasksCompleted != _urls.Count)
            {
                Task.Run(() =>
               {
                   await GetContentFromUrlAsync(queue.Dequeue());

               });

            }

        }

        private async Task<string> GetContentFromUrlAsync(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
            return await client.GetStringAsync(url);
        }
    }
}