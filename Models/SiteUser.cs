using System.ComponentModel.DataAnnotations;

namespace Projekt.NET.Models
{
    public class SiteUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string Username { get; set; }

        [Required]
        [Display(Name ="Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Rola")]
        public int RoleId { get; set; }

        [Display(Name = "Rola")]
        public Role Role { get; set; }
    }
}
