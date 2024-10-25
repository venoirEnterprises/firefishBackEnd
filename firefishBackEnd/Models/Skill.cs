using System.ComponentModel.DataAnnotations;

namespace firefishBackEnd.Models
{
    public class Skill
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
