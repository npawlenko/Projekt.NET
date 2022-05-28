using Projekt.NET.Models;

namespace Projekt.NET.DAL
{
    public interface IDatabase
    {
        public List<Product> ProductList();
        public Product GetProduct(int _id);
        public int UpdateProduct(Product _product);
        public int DeleteProduct(int _id);
        public int AddProduct(Product _product);

        public List<Category> CategoryList();
        public Category GetCategory(int _id);
        public int UpdateCategory(Category _product);
        public int DeleteCategory(int _id);
        public int AddCategory(Category _product);


        public bool ValidateUser(SiteUser user);
        public SiteUser GetUser(int _id);
        public int UpdateUser(SiteUser user);
        public int DeleteUser(int _id);
        public int AddUser(SiteUser user);

        public Role GetRole(int _id);
    }
}
