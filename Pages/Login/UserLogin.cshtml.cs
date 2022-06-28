using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;
using System.Security.Claims;

namespace Projekt.NET.Pages.Login
{
    public class UserLoginModel : MyPageModel
    {
        public UserLoginModel(IDatabase db) : base(db)
        {
        }

        public string Message { get; set; }

        [BindProperty]
        public SiteUser user { get; set; }


        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (user.Password.Equals("") || user.Username.Equals(""))
                return Page();

            user.Password = hash(user.Password);

            if (_db.ValidateUser(user))
            {
                SiteUser dbUser = _db.GetUserByName(user.Username);
                Role role = _db.GetRole(dbUser.RoleId);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, dbUser.Username),
                    new Claim(ClaimTypes.Role, role.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));



                if (returnUrl == null)
                    return RedirectToPage("/Admin/Index");
                else
                    return Redirect(returnUrl);
            }

            Message = "Podane dane logowania s¹ nieprawid³owe!";
            return Page();
        }
    }
}
