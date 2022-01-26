using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Films.Models.Web.BingSearch
{
    public interface IBingElement
    {
        string SearchParametrs { get; }
        IAsyncEnumerable<string> GetWorkingLinksAsync(string htmlСontent);
        string GetObjectType();
    }
}
