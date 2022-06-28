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
    public class IndexModel : MyPageModel
    {
        public IndexModel(IDatabase db) : base(db)
        {
        }

        public IList<Product> Product { get;set; } = default!;

        public IActionResult OnGet()
        {
            List<Product> products = _db.ProductList();
            if (products != null)
            {
                Product = products;
            }

            return Page();
        }


        public string DisplayProductCategories(Product p)
        {
            String result = "";
            List<Category> names = (List<Category>)_db.GetProductCategories(p);
            foreach (var c in names)
            {
                result += c.Name + ", ";
            }
            result = result.Substring(0, result.Length - 2);

            return result;
        }
    }
}
