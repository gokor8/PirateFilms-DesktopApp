using System.Collections.Generic;
using System.Threading.Tasks;

namespace Films.Models.Web
{
    public interface ISearcher
    {
        public IEnumerable<string> WorkingSites { get; }
        public string WorkingSite { get; }
        public Task SearchWorkingSitesAsync();
    }
}