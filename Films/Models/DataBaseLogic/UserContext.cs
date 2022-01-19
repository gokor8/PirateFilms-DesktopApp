using System.Data.Entity;

namespace Films.MVVMLogic.Models.DataBaseLogic
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DbConnection")
        {

        }

        public DbSet<User> Users { get; set; }
    }
}