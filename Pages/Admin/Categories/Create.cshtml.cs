using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.NET.DAL;
using Projekt.NET.Data;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Admin.Categories
{
    public class CreateModel : MyPageModel
    {
        public CreateModel(IDatabase db) : base(db)
        {
        }

        public IActionResult OnGet()
        {
            if (!HttpContext.User.IsInRole("Admin") && !HttpContext.User.IsInRole("Kierownik"))
                return RedirectToPage("./Index");

            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
          if (!ModelState.IsValid || Category == null)
            {
                return Page();
            }

            _db.UpdateCategory(Category);

            return RedirectToPage("./Index");
        }
    }
}
