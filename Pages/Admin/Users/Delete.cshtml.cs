using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Admin.Users
{
    public class DeleteModel : MyPageModel
    {
        public DeleteModel(IDatabase db) : base(db)
        {
        }

        public IActionResult OnGet(int id)
        {
            if(!HttpContext.User.IsInRole("Admin"))
                return RedirectToPage("./Index");

            SiteUser user = _db.GetUser(id);
            Role role = _db.GetRole(user.RoleId);
            if(role.Name.Equals("Admin"))
                return RedirectToPage("./Index");
            
            
            _db.DeleteUser(id);
            return RedirectToPage("./Index");
        }
    }
}
