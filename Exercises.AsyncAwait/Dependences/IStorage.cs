﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait.Dependences
{
    public interface IStorage
    {
        Task<IDictionary<string, string>> GetUrlsFromStorageAsync();

        Task SaveContentToStorageAsync(string url, string content);
    }
}