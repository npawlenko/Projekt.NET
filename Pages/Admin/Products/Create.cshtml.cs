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

namespace Projekt.NET.Pages.Admin.Products
{
    public class CreateModel : MyPageModel
    {
        [BindProperty]
        public Product Product { get; set; } = default!;
        public SelectList Categories { get; set; }


        public CreateModel(IDatabase db) : base(db)
        {
        }

        public IActionResult OnGet()
        {
            if (!HttpContext.User.IsInRole("Admin") && !HttpContext.User.IsInRole("Kierownik"))
                return RedirectToPage("./Index");

            List<Category> _list = _db.CategoryList();
            Categories = new SelectList(_list, "Id", "Name");

            return Page();
        }

        


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Product == null)
            {
                return Page();
            }


            int productId = _db.AddProduct(Product);


            String _categories = Request.Form["Categories"];
            if (_categories == null) return Page();

            String[] categoryIds = _categories.Split(',');
            for (int i = 0; i < categoryIds.Length; i++)
            {
                _db.LinkProduct(productId, Int32.Parse(categoryIds[i]));
            }

            return RedirectToPage("./Index");
        }
    }
}
