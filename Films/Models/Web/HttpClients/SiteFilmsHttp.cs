using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Films.Models.Web.BingSearch;

namespace Films.Models.Web.HttpClients
{
    public sealed class SiteFilmsHttp : BaseHttp
    {
        private static SiteFilmsHttp _instance;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        private string _workingBaseAddress = string.Empty;

        private SiteFilmsHttp()
        {

        }

        public async Task GetBaseAddressAsync()
        {
            var searcher = await new SiteSearcherFactory().CreateSiteSearcherAsync();

            _workingBaseAddress = searcher.WorkingLinks.First();
        }

        public static async Task<SiteFilmsHttp> GetInstanceAsync()
        {
            await _semaphore.WaitAsync().ConfigureAwait(false);
            try
            {
                if (_instance == null)
                {
                    _instance = new SiteFilmsHttp();
                    await _instance.GetBaseAddressAsync().ConfigureAwait(false);
                    _instance.SetOptionalHeaders();
                }
            }
            finally
            {
                _semaphore.Release();
            }

            return _instance;
        }

        public override void SetOptionalHeaders()
        {
            Client.BaseAddress = new Uri(_workingBaseAddress);
            Client.DefaultRequestHeaders.Host = Client.BaseAddress.Host;
        }
    }
}
