using System.Collections.Generic;

namespace Films.Models.Web.BingSearch
{
    public interface IBingParser
    {
        string SearchParametrs { get; }

        IAsyncEnumerable<string> GetWorkingLinksAsync(string htmlСontent);

        string GetObjectType();
    }
}
