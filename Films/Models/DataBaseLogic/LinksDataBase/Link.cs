using System.ComponentModel.DataAnnotations;

namespace Films.MVVMLogic.Models.DataBaseLogic.LinksDataBase
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        public string WorkingLink { get; set; }
    }
}