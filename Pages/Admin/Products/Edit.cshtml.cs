using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.NET.DAL;
using Projekt.NET.Models;

namespace Projekt.NET.Pages.Admin.Products
{
    public class EditModel : MyPageModel
    {
        public EditModel(IDatabase db) : base(db)
        {
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public SelectList Categories { get; set; }

        public IActionResult OnGet(int id = 0)
        {
            if (!HttpContext.User.IsInRole("Admin") && !HttpContext.User.IsInRole("Kierownik"))
                return RedirectToPage("./Index");

            if (id == 0)
            {
                return NotFound();
            }

            var product =  _db.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;

            List<Category> _list = _db.CategoryList();
            Categories = new SelectList(_list, "Id", "Name");

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

            _db.UpdateProduct(Product);

            //kategorie
            _db.UnlinkProduct(Product.Id);

            String _categories = Request.Form["Categories"];
            if (_categories == null) return Page();

            String[] categoryIds = _categories.Split(',');
            for (int i = 0; i < categoryIds.Length; i++)
            {
                _db.LinkProduct(Product.Id, Int32.Parse(categoryIds[i]));
            }

            return RedirectToPage("./Index");
        }
    }
}
