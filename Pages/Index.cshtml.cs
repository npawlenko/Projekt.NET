using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages
{
    public class IndexModel : MyPageModel
    {
        public IndexModel(IProductDB productDB, ICategoryDB categoryDB) : base(productDB, categoryDB)
        {
        }


        public void OnGet()
        {
            
        }
    }
}