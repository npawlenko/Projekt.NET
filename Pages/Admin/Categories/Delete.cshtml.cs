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
    public class DeleteModel : MyPageModel
    {
        public DeleteModel(IDatabase db) : base(db)
        {
        }

        [BindProperty]
      public Category Category { get; set; } = default!;

        public IActionResult OnGet(int id = 0)
        {
            if (!HttpContext.User.IsInRole("Admin") && !HttpContext.User.IsInRole("Kierownik"))
                return RedirectToPage("./Index");

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

        public IActionResult OnPost(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var category = _db.GetCategory(id);

            if (category != null)
            {
                Category = category;
                _db.DeleteCategory(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
