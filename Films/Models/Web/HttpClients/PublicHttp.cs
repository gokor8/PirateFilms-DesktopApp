using System.IO;
using System.Threading.Tasks;

namespace Films.Models.Web.HttpClients
{
    public class PublicHttp : BaseHttp
    {
        private static PublicHttp _instance = new PublicHttp();

        public static PublicHttp GetInstance()
        {
            return _instance;
        }

        public override void SetOptionalHeaders()
        { }
    }
}
