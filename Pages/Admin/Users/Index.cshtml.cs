using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Admin.Users
{
    public class IndexModel : MyPageModel
    {
        public IndexModel(IDatabase db) : base(db)
        {
        }

        public List<SiteUser> User = new List<SiteUser>();
        public IActionResult OnGet()
        {
            if (!HttpContext.User.IsInRole("Admin"))
                return RedirectToPage("/Admin/Index");

            User = _db.UserList();
            return Page();
        }
    }
}
