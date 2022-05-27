using System.ComponentModel.DataAnnotations;

namespace Projekt.NET.Models
{
    public class Product
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nazwa produktu")]
        [Required(ErrorMessage = "Pole nazwa produktu jest wymagane!")]

        public string Name { get; set; }

        [Display(Name ="Opis")]
        [Required(ErrorMessage ="Pole opis jest wymagane!")]
        public string Description { get; set; }

        [Display(Name ="Adres obrazka")]
        public string ImgPath { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Pole cena jest wymagane!")]
        public decimal Price { get; set; }


        public IList<CategoryProduct>? CategoryProduct { get; set; }
    }
}
