using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages
{
    public class ProductModel : MyPageModel
    {
        public ProductModel(IDatabase db) : base(db)
        {
        }

        public Product Product { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            var product = _db.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }

        public String DisplayProductCategories()
        {
            String result = "";
            List<Category> names = (List<Category>)_db.GetProductCategories(Product);
            foreach (var c in names)
            {
                result += c.Name + ", ";
            }
            if(result.Length > 0)
                result = result.Substring(0, result.Length - 2);

            return result;
        }
    }
}
