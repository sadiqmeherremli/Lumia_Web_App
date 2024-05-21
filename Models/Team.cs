using System.ComponentModel.DataAnnotations.Schema;

namespace Lumia.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string FUllName { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }

        [NotMapped]
        public IFormFile? ImgFile { get; set; }
        
    }
}
