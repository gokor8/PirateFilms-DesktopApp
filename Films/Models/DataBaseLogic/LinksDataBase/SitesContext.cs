using System.Data.Entity;
using Films.MVVMLogic.Models.DataBaseLogic.LinksDataBase;

namespace Films.Models.DataBaseLogic.LinksDataBase
{
    public class SitesContext : DbContext
    {
        public SitesContext() : base("DbConnection")
        {

        }

        public DbSet<Link> Links { get; set; }
    }
}