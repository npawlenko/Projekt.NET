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
    public class DetailsModel : MyPageModel
    {
        public DetailsModel(IDatabase db) : base(db)
        {
        }

        public Category Category { get; set; } = default!; 

        public IActionResult OnGet(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var category = _db.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            else 
            {
                Category = category;
            }
            return Page();
        }
    }
}
