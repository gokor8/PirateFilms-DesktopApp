using System.IO;
using System.Threading.Tasks;

namespace Films.Models.Web.HttpClients
{
    public class PublicHttp : BaseHttp
    {
        private static PublicHttp _instance = new PublicHttp();
        public override Task<Stream> GetStreamClient(string link)
        {
            return Client.GetStreamAsync(link);
        }

        public static PublicHttp GetInstance()
        {
            return _instance;
        }
    }
}
