using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.NET.Data;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly Projekt.NET.Data.ProjektNETContext _context;

        public IndexModel(Projekt.NET.Data.ProjektNETContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Product != null)
            {
                Product = await _context.Product.ToListAsync();
            }
        }
    }
}
