using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using System.Security.Cryptography;
using System.Text;

namespace Projekt.NET.Models
{
    public class MyPageModel : PageModel
    {
        public IDatabase _db;
        public Cart cart { get; set; }

        public MyPageModel(IDatabase db)
        {
            _db = db;
            cart = new Cart(db);
        }



        public void LoadCart()
        {
            string cartContents = HttpContext.Request.Cookies["cart"];
            if (cartContents != null)
                cart.Load(cartContents);
        }

        public void SaveCart()
        {
            string cartContents = cart.Save();

            CookieOptions co = new CookieOptions();
            co.Expires = DateTime.Now.AddMinutes(180);
            Response.Cookies.Append("cart", cartContents, co);
        }



        public string hash(string rawData)
        {
            String result;

            using (SHA256 hash = SHA256.Create())
            {
                var byteArrayResultOfRawData = Encoding.UTF8.GetBytes(rawData);
                var byteArrayResult = hash.ComputeHash(byteArrayResultOfRawData);
                result = string.Concat(Array.ConvertAll(byteArrayResult, 
                    h => h.ToString("X2")
                ));
            }

            return result;
        }
    }
}
