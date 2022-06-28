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
    public class DetailsModel : MyPageModel
    {
        public DetailsModel(IDatabase db) : base(db)
        {
        }

        public Product Product { get; set; } = default!; 

        public IActionResult OnGet(int id = 0)
        {
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

        public String DisplayProductCategories()
        {
            String result = "";
            List<Category> names = (List<Category>)_db.GetProductCategories(Product);
            foreach (var c in names)
            {
                result += c.Name + ", ";
            }
            result = result.Substring(0, result.Length - 2);

            return result;
        }
    }
}
