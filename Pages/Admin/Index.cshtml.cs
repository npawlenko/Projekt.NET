using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Admin
{
    public class IndexModel : MyPageModel
    {
        public IndexModel(IDatabase db) : base(db)
        {
        }

        public void OnGet()
        {
        }
    }
}
