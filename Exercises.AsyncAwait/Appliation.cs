using Exercises.AsyncAwait.Dependences;
using System.Threading.Tasks;
using Unity;

namespace Exercises.AsyncAwait
{
    class Appliation
    {
        private UnityContainer _container;
        public Appliation(UnityContainer container)
        {
            _container = container;
        }

        public async Task Run ()
        {
            var logger = _container.Resolve<ILogger>();
            var storage = _container.Resolve<IStorage>();
            var urlSaver = _container.Resolve<IUrlSaver>();
            var statusLine = _container.Resolve<IStatusReporter>();

            urlSaver.UpdateStatusLine += ((s, e) =>
            {
                statusLine.Update(e.Delta);
                logger.WriteRedText($"Current status {statusLine.CurrentStatus} % ");
            });

            if (await urlSaver.TryGetGetUrlsFromStorageAsync())
            {
                await urlSaver.GetDataFromUrlAsync(5);
                await urlSaver.SaveContentToStorageAsync();
                ConsoleWithLocker.WriteLine("************** ALL DONE**************");
            }
        }
    }
}
