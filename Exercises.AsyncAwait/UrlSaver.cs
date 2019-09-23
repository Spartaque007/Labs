using Exercises.AsyncAwait.Dependences;
using System;
using System.Collections.Concurrent;
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
        private readonly ConcurrentQueue<string> _queue;

        private IDictionary<string, string> _urls;
        private decimal _quantumOfStatusLine;
        private int _workingTasks;
        private object _locker;
        private int _counter;

        public event EventHandler<StatusLineEventArgs> UpdateStatusLine;


        public UrlSaver(IStorage storage, ILogger logger)
        {
            _storage = storage;
            _logger = logger;
            _urls = new Dictionary<string, string>();
            _queue = new ConcurrentQueue<string>();
            _workingTasks = 0;
            _taskCompletionSource = new TaskCompletionSource<int>();
            _counter = 0;
            _locker = new Object();
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

            var tasksInTimeQuantity = Math.Min(_urls.Count, downloadsQuantity);

            for (int i = 0; i < tasksInTimeQuantity; i++)
            {
                TaskRunAsync();
            }

            return await t;
        }

        public async Task SaveContentToStorageAsync()
        {
            foreach (var url in _urls)
            {
                await _storage.SaveContentToStorageAsync(url.Key, url.Value);
            }
        }


        private async void TaskRunAsync()
        {
            _workingTasks++;
            string url;
            var DequeueWasSuccess = _queue.TryDequeue(out url);

            if (!DequeueWasSuccess)
            {
                _logger.WriteRedText("Aaaaaaaaaaaaaaaaaaaa is bad");
            }

            await GetContentFromUrlAsync(url);

            lock (_locker)
            {
                _workingTasks--;
            }

            lock (_locker)
            {
                if (_queue.Count != 0)
                {
                    TaskRunAsync();
                }
                else if (_workingTasks == 0)
                {
                    _taskCompletionSource.SetResult(1);
                }
            }
        }

        private async Task GetContentFromUrlAsync(string url)
        {
            _logger.Write("Start new download");
            var response = await GetPageAsync(url);
            string downloadStatus;

            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    downloadStatus = "successful";

                    if (response.Content != null)
                    {

                        string content = await response.Content?.ReadAsStringAsync();

                        _urls[url] = content;
                    }
                }
                else
                {
                    downloadStatus = $"with fault (status code {response.StatusCode})";
                }
                _logger.Write($"Download finished {downloadStatus}.");
            }

            lock (_locker)
            {
                _counter++;
                UpdateStatusLine.Raise(this, new StatusLineEventArgs(_quantumOfStatusLine));
                _logger.Write($" {_queue.Count} downloads in queue ");
            }
        }

        private async Task<HttpResponseMessage> GetPageAsync(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
                    var response = await client.GetAsync(url);

                    return response;
                }
                catch (InvalidOperationException e)
                when (e.Message == "An invalid request URI was provided. The request URI must either be an absolute URI or BaseAddress must be set.")
                {
                    _logger.Write(e.Message);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                }
            }
        }
    }
}