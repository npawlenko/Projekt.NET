    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Cart
{
    public class IndexModel : MyPageModel
    {
        public List<Product> products { get; set; }

        public IndexModel(IDatabase db) : base(db)
        {
        }

        public void OnGet()
        {
            LoadCart();
            products = cart.List();
        }
    }
}
