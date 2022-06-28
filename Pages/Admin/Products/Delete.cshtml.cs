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

namespace Projekt.NET.Pages.Admin.Products
{
    public class DeleteModel : MyPageModel
    {
        public DeleteModel(IDatabase db) : base(db)
        {
        }

        [BindProperty]
        public Product Product { get; set; } = default!;


        public IActionResult OnGet(int id = 0)
        {
            if (!HttpContext.User.IsInRole("Admin") && !HttpContext.User.IsInRole("Kierownik"))
                return RedirectToPage("./Index");

            if (id == 0)
            {
                return NotFound();
            }

            var product = _db.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public IActionResult OnPost(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var product = _db.GetProduct(id);

            if (product != null)
            {
                Product = product;
                _db.DeleteProduct(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
