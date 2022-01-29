using System.Collections.Generic;
using System.Threading.Tasks;

namespace Films.Models.Web.BingSearch
{
    public interface ISearcher
    { 
        IEnumerable<string> WorkingLinks { get; }

        Task SearchWorkingSitesAsync(bool takeAll = false);
    }
}