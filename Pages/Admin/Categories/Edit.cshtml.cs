using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.NET.DAL;
using Projekt.NET.Data;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Admin.Categories
{
    public class EditModel : MyPageModel
    {
        public EditModel(IDatabase db) : base(db)
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
            Category = category;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.UpdateCategory(Category);

            return RedirectToPage("./Index");
        }
    }
}
