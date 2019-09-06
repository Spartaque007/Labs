using System;
using Exercises.AsyncAwait.Dependences;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    public sealed class UrlSaver
    {
        private readonly IStorage _storage;
        private readonly ILogger _logger;
        private readonly TaskCompletionSource<int> _taskCompletionSource;
        private readonly Queue<string> _queue;

        private IDictionary<string, string> _urls;
        private decimal _quantumOfStatusLine;
        private int _workingTasks;

        public event EventHandler<StatusLineEventArgs> UpdateStatusLine;


        public UrlSaver(IStorage storage, ILogger logger)
        {
            _storage = storage;
            _logger = logger;
            _urls = new Dictionary<string, string>();
            _queue = new Queue<string>();
            _workingTasks = 0;
            _taskCompletionSource = new TaskCompletionSource<int>();
        }


        public async Task<bool> TryGetGetUrlsFromStorageAsync()
        {
            _urls = await _storage.GetUrlsFromStorageAsync();
            return _urls.Count > 0;
        }

        public async Task<int> GetDataFromUrlAsync(int downloadsQuantity)
        {
            var t = _taskCompletionSource.Task;

            foreach (var url in _urls)
            {
                _queue.Enqueue(url.Key);
            }
            _quantumOfStatusLine = 100m / _queue.Count;
            _logger.Write($"In queue {_queue.Count} downloads");

            var tasksInTimeQuantity = downloadsQuantity >= _urls.Count
                ? _urls.Count
                : downloadsQuantity;

            for (int i = 0; i < tasksInTimeQuantity; i++)
            {
                TaskRun();
                _workingTasks++;
            }

            return await t;
        }

        private void TaskRun()
        {
            var newTask = GetContentFromUrlAsync(_queue.Dequeue());
            newTask.ContinueWith((e) =>
            {
                _workingTasks--;
                if (_queue.Count != 0)
                {
                    TaskRun();
                    _workingTasks++;
                }
                else if (_workingTasks == 0)
                {
                    _taskCompletionSource.SetResult(1);
                }
            });
        }

        public async Task SaveContentToStorageAsync()
        {
            foreach (var url in _urls)
            {
                await _storage.SaveContentToStorageAsync(url.Key, url.Value);
            }
        }


        private async Task GetContentFromUrlAsync(string url)
        {
            _logger.Write("Start new download");
            _urls[url] = await GetPage(url);
            _logger.Write($"Download finished. {_queue.Count} downloads in queue ");
            UpdateStatusLine?.Invoke(this, new StatusLineEventArgs(_quantumOfStatusLine));
        }

        private async Task<string> GetPage(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
                return await client.GetStringAsync(url);
            }
        }
    }
}