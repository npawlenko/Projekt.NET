using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.NET.DAL;
using System.Security.Cryptography;
using System.Text;

namespace Projekt.NET.Models
{
    public class MyPageModel : PageModel
    {
        public IProductDB productDB;
        public ICategoryDB categoryDB;

        public MyPageModel(IProductDB productDB, ICategoryDB categoryDB)
        {
            this.productDB = productDB;
            this.categoryDB = categoryDB;
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
