using Projekt.NET.Models;
using System.Text.Json;

namespace Projekt.NET.DAL
{
    public class Cart
    {
        public List<Product> products;
        public IDatabase _db;

        public Cart(IDatabase db)
        {
            _db = db;
            products = new List<Product>();
        }


        public void Load(string jsonProducts)
        {
            if (jsonProducts != null)
            {
                products = JsonSerializer.Deserialize<List<Product>>(jsonProducts);
                List<Product> productList = _db.ProductList();

                for (int i = 0; i < products.Count; i++)
                {
                    Product p = products[i];
                    if (! productList.Contains(p))
                        Remove(p);
                }
            }
        }

        public string Save()
        {
            return JsonSerializer.Serialize(products);
        }

        public void Add(Product p)
        {
            products.Add(p);
        }

        public void Remove(Product p)
        {
            products.Remove(p);
        }

        public void Clear()
        {
            products = new List<Product>();
        }

        public List<Product> List()
        {
            return products;
        }
    }
}
