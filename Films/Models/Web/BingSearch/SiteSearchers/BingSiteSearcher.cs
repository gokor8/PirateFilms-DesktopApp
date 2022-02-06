using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Films.Models.DataBaseLogic.LinksDataBase;
using Films.Models.Web.BingSearch.Factories;
using Films.MVVMLogic.Models.DataBaseLogic.LinksDataBase;

namespace Films.Models.Web.BingSearch.SiteSearchers
{
    public class BingSiteSearcher : ISearcher
    {
        private const string SeachSiteName = "lordfilm";
        public IEnumerable<string> WorkingLinks { get; private set; }
        public HashSet<Task> TrackedTasks { get; } = new HashSet<Task>();

        public async Task SearchWorkingSitesAsync(bool takeAll = false)
        {
            IBingFactory bingFactory = new SearchBingFactory();

            var bingRequestHtml = await new Bing()
                .GetSearchResultAsync(SeachSiteName, bingFactory.CreateBingSettings());

            var bingParser = bingFactory.CreateBingParser();

            var asyncLinkCollection = takeAll
                ? bingParser.GetWorkingLinksAsync(bingRequestHtml)
                : bingParser.GetWorkingLinksAsync(bingRequestHtml).Take(1);

            WorkingLinks = await asyncLinkCollection.ToListAsync();

            TrackedTasks.Add(Task.Run(()=>
                refreshDatabaseLinks(WorkingLinks))
            );
        }

        private void refreshDatabaseLinks(IEnumerable<string> workingLinks)
        {
            //Передаю сюда ссылку на объект, чтобы не было проблем с доступом в многопоточности
            using (var context = new SitesContext())
            {
                foreach (var workingLink in workingLinks)
                {
                    if(context.Links.Count(l=>l.WorkingLink == workingLink) <= 0)
                        context.Links.AddOrUpdate(new Link() { WorkingLink = workingLink });
                }   

                context.SaveChanges();
            }
        }
    }
}