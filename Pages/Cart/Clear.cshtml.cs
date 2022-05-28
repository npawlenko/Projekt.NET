using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Cart
{
    public class ClearModel : MyPageModel
    {
        public ClearModel(IDatabase db) : base(db)
        {
        }

        public IActionResult OnGet()
        {
            LoadCart();
            cart.Clear();
            SaveCart();

            return RedirectToPage("Cart");
        }
    }
}
