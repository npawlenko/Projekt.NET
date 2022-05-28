using System.ComponentModel.DataAnnotations;

namespace Projekt.NET.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Kategoria")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImgPath { get; set; }

        public IList<CategoryProduct>? CategoryProduct { get; set; }
    }
}
