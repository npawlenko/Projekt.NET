using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Cart
{
    public class AddModel : MyPageModel
    {
        public AddModel(IDatabase db) : base(db)
        {
        }

        public IActionResult OnGet(int id)
        {
            Product p = _db.GetProduct(id);

            LoadCart();
            cart.Add(p);
            SaveCart();

            return RedirectToPage("./Index");
        }
    }
}
