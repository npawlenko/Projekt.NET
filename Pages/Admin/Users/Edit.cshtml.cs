using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Admin.Users
{
    public class EditModel : MyPageModel
    {
        public EditModel(IDatabase db) : base(db)
        {
        }

        [BindProperty]
        public SiteUser User { get; set; }
        public SelectList Roles { get; set; }
        public string Message { get; set; }


        public IActionResult OnGet(int id)
        {
            if (!HttpContext.User.IsInRole("Admin"))
                return RedirectToPage("./Index");

            List<Role> roles = _db.RoleList();
            Roles = new SelectList(roles, "Id", "Name");

            User = _db.GetUser(id);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!HttpContext.User.IsInRole("Admin"))
                return RedirectToPage("./Index");

            if (!Request.Form["vpassword"].Equals(User.Password))
            {
                Message = "Podane has³a siê nie zgadzaj¹!";
                return Page();
            }

            int role = Int32.Parse(Request.Form["role"]);
            User.Password = hash(User.Password);
            User.RoleId = role;
            _db.UpdateUser(User);

            return RedirectToPage("./Index");
        }
    }
}
