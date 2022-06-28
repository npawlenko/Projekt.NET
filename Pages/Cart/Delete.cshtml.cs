using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Cart
{
    public class DeleteModel : MyPageModel
    {
        public DeleteModel(IDatabase db) : base(db)
        {
        }

        public IActionResult OnGet(int id)
        {
            LoadCart();

            cart.List().RemoveAt(id);
            SaveCart();
            return RedirectToPage("/Cart/Index");
        }
    }
}
