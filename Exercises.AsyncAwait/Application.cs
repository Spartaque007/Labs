using Exercises.AsyncAwait.Dependences;
using System.Threading.Tasks;

namespace Exercises.AsyncAwait
{
    public class Application
    {
        private readonly ILogger _logger;
        private readonly IUrlSaver _urlSaver;
        private readonly IStatusReporter _statusReporter;


        public Application(ILogger logger, IUrlSaver urlSaver, IStatusReporter statusReporter)
        {
            _logger = logger;
            _statusReporter = statusReporter;
            _urlSaver = urlSaver;
        }

        public async Task Run()
        {
            _urlSaver.UpdateStatusLine += ((s, e) =>
            {
                _statusReporter.Update(e.Delta);
                _logger.WriteRedText($"Current status {_statusReporter.CurrentStatus} % ");
            });

            if (await _urlSaver.TryGetGetUrlsFromStorageAsync())
            {
                await _urlSaver.GetDataFromUrlAsync(5);
                await _urlSaver.SaveContentToStorageAsync();
                ConsoleWithLocker.WriteLine("************** ALL DONE**************");
            }
        }
    }
}