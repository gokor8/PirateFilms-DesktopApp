using System.Collections.Generic;

namespace Films.Models.Web
{
    public interface ISearcher
    {
        public IAsyncEnumerable<string> SearchWorkingSitesAsync();
    }
}