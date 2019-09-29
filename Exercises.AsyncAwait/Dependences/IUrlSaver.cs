using System;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    public interface IUrlSaver
    {
        event EventHandler<StatusLineEventArgs> UpdateStatusLine;

        Task<int> GetDataFromUrlAsync(int downloadsQuantity);
        Task SaveContentToStorageAsync();
        Task<bool> TryGetGetUrlsFromStorageAsync();
    }
}