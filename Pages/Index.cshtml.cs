using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages
{
    public class IndexModel : MyPageModel
    {
        public List<Product> products { get; set; }

        public IndexModel(IDatabase _db) : base(_db)
        {
        }


        public void OnGet()
        {
            products = _db.ProductList();
        }
    }
}