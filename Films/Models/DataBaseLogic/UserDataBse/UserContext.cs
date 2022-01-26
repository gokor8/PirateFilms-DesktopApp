using System.Data.Entity;
using Films.MVVMLogic.Models.DataBaseLogic;

namespace Films.Models.DataBaseLogic.UserDataBse
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DbConnection")
        {

        }

        public DbSet<User> Users { get; set; }
    }
}