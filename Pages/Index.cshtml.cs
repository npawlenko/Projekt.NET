using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages
{
    public class IndexModel : MyPageModel
    {
        public IndexModel(IDatabase _db) : base(_db)
        {
        }


        public void OnGet()
        {
            
        }
    }
}