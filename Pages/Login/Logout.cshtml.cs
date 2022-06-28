using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Login
{
    public class LogoutModel : MyPageModel
    {
        public LogoutModel(IDatabase db) : base(db)
        {
        }

        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            return this.RedirectToPage("/Index");
        }
    }
}
