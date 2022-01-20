using System.ComponentModel.DataAnnotations;

namespace Films.MVVMLogic.Models.DataBaseLogic
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}