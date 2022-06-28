using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages
{
    public class ProductsModel : MyPageModel
    {
        public List<Category> categories { get; set; }

        public ProductsModel(IDatabase db) : base(db)
        {
        }

        public void OnGet()
        {
            categories = _db.CategoryList();
        }
    }
}
