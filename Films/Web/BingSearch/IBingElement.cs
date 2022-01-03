using System.Threading.Tasks;

namespace Films.Web.BingSearch
{
    public interface IBingElement
    {
        string SearchParametrs { get; }

        Task<string> GetWorkingLink(string htmlСontent);

        string GetObjectType();
    }
}
