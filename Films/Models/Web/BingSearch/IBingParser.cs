using System.Collections.Generic;

namespace Films.Models.Web.BingSearch
{
    public interface IBingParser
    {
        IAsyncEnumerable<string> GetWorkingLinksAsync(string htmlСontent);
    }
}
