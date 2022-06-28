using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.NET.DAL;
using Projekt.NET.Data;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Admin.Categories
{
    public class IndexModel : MyPageModel
    {
        public IndexModel(IDatabase db) : base(db)
        {
        }

        public IList<Category> Category { get;set; } = default!;

        public void OnGet()
        {
            Category = _db.CategoryList(); 
        }
    }
}
