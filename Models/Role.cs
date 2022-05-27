using System.ComponentModel.DataAnnotations;

namespace Projekt.NET.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Rola")]
        public string Name { get; set; }

        public ICollection<SiteUser> Users { get; set; }
    }
}
