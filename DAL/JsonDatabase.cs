using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Projekt.NET.Models;
using System.Linq;

namespace Projekt.NET.DAL
{
    public class JsonDatabase : IDatabase
    {
        private JObject _db;
        private const string file = "db/jsonDb.json";

        public JsonDatabase()
        {
            Load();
        }


        public void Load()
        {
            string jsonData = System.IO.File.ReadAllText(file);
            JObject obj = JObject.Parse(jsonData);
            _db = obj;
        }

        public void Save()
        {
            File.WriteAllText(file, _db.ToString());
        }




        public int AddCategory(Category _category)
        {
            JObject p = JObject.FromObject(_category);
            _category.Id = NewId("Categories");
            _db.SelectToken("Categories").Append(p);
            Save();

            return 1;
        }

        public int AddProduct(Product _product)
        {
            JObject p = JObject.FromObject(_product);
            _product.Id = NewId("Products");
            _db.SelectToken("Products").Append(p);
            Save();

            return 1;
        }

        public List<Category> CategoryList()
        {
            List<Category> categories = new List<Category>();

            foreach (JToken child in _db.SelectToken("Categories").Children())
            {
                JObject JCategory = JObject.Parse(child.ToString());

                int id = Int32.Parse(JCategory.GetValue("Id").ToString());
                string name = JCategory.GetValue("Name").ToString();

                Category Category = new Category
                {
                    Id = id,
                    Name = name
                };

                categories.Add(Category);
            }

            return categories;
        }

        public int DeleteCategory(int _id)
        {
            foreach(JToken child in _db.SelectToken("Categories").Children()) {
                int id = Int32.Parse(JObject.Parse(child.ToString()).GetValue("Id").ToString());
                if(id == _id)
                {
                    child.Remove();
                    Save();
                    UnlinkCategory(_id);
                    return 1;
                }
            }

            return 0;
        }

        public int DeleteProduct(int _id)
        {
            foreach (JToken child in _db.SelectToken("Products").Children())
            {
                int id = Int32.Parse(JObject.Parse(child.ToString()).GetValue("Id").ToString());
                if (id == _id)
                {
                    child.Remove();
                    Save();
                    UnlinkProduct(_id);
                    return 1;
                }
            }

            return 0;
        }

        public Category GetCategory(int _id)
        {
            foreach (JToken child in _db.SelectToken("Categories").Children())
            {
                JObject JCategory = JObject.Parse(child.ToString());

                int id = Int32.Parse(JCategory.GetValue("Id").ToString());
                if (id == _id)
                {
                    string name = JCategory.GetValue("Name").ToString();

                    Category Category = new Category
                    {
                        Id = id,
                        Name = name
                    };

                    return Category;
                }
            }

            return null;
        }

        public Product GetProduct(int _id)
        {
            foreach (JToken child in _db.SelectToken("Products").Children())
            {
                JObject JProduct = JObject.Parse(child.ToString());

                int id = Int32.Parse(JProduct.GetValue("Id").ToString());
                if (id == _id)
                {
                    string name = JProduct.GetValue("Name").ToString();
                    string description = JProduct.GetValue("Description").ToString();
                    string imgPath = JProduct.GetValue("ImgPath").ToString();
                    decimal price = Decimal.Parse(JProduct.GetValue("Price").ToString());

                    Product Product = new Product
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        ImgPath = imgPath,
                        Price = price
                    };
                }
            }

            return null;
        }

        public List<Product> ProductList()
        {
            List<Product> products = new List<Product>();

            foreach (JToken child in _db.SelectToken("Products").Children())
            {
                JObject JProduct = JObject.Parse(child.ToString());

                int id = Int32.Parse(JProduct.GetValue("Id").ToString());
                string name = JProduct.GetValue("Name").ToString();
                string description = JProduct.GetValue("Description").ToString();
                string imgPath = JProduct.GetValue("ImgPath").ToString();
                decimal price = Decimal.Parse(JProduct.GetValue("Price").ToString());

                Product Product = new Product
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    ImgPath = imgPath,
                    Price = price
                };

                products.Add(Product);
            }

            return products;
        }

        public int UpdateCategory(Category _category)
        {
            foreach (JToken child in _db.SelectToken("Products").Children())
            {
                JObject JProduct = JObject.Parse(child.ToString());

                int id = Int32.Parse(JProduct.GetValue("Id").ToString());
                if (id == _category.Id)
                {
                    child.Remove();
                    JObject c = JObject.FromObject(_category);
                    _db.SelectToken("Categories").Append(c);
                    Save();

                    return 1;
                }
            }

            return 0;
        }

        public int UpdateProduct(Product _product)
        {
            foreach (JToken child in _db.SelectToken("Products").Children())
            {
                JObject JProduct = JObject.Parse(child.ToString());

                int id = Int32.Parse(JProduct.GetValue("Id").ToString());
                if (id == _product.Id)
                {
                    child.Remove();
                    JObject p = JObject.FromObject(_product);
                    _db.SelectToken("Products").Append(p);
                    Save();

                    return 1;
                }
            }

            return 0;
        }

        public bool ValidateUser(SiteUser user)
        {
            foreach (JToken child in _db.SelectToken("Users").Children())
            {
                JObject JUser = JObject.Parse(child.ToString());

                int id = Int32.Parse(JUser.GetValue("Id").ToString());
                if (id == user.Id)
                {
                    if (JUser.GetValue("Password").Equals(user.Password))
                        return true;
                }
            }

            return false;
        }

        public SiteUser GetUser(int _id)
        {
            foreach (JToken child in _db.SelectToken("Users").Children())
            {
                JObject JUser = JObject.Parse(child.ToString());

                int id = Int32.Parse(JUser.GetValue("Id").ToString());
                if (id == _id)
                {
                    string name = JUser.GetValue("Username").ToString();
                    string password = JUser.GetValue("Password").ToString();
                    int roleId = Int32.Parse(JUser.GetValue("roleId").ToString());

                    SiteUser user = new SiteUser
                    {
                        Id = id,
                        Username = name,
                        Password = password,
                        RoleId = roleId,
                        Role = GetRole(roleId)
                    };

                    return user;
                }
            }

            return null;
        }

        public int UpdateUser(SiteUser user)
        {
            foreach (JToken child in _db.SelectToken("Users").Children())
            {
                JObject JUser = JObject.Parse(child.ToString());

                int id = Int32.Parse(JUser.GetValue("Id").ToString());
                if (id == user.Id)
                {
                    child.Remove();
                    JObject p = JObject.FromObject(user);
                    _db.SelectToken("Users").Append(p);
                    Save();

                    return 1;
                }
            }

            return 0;
        }

        public int DeleteUser(int _id)
        {
            foreach (JToken child in _db.SelectToken("Users").Children())
            {
                int id = Int32.Parse(JObject.Parse(child.ToString()).GetValue("Id").ToString());
                if (id == _id)
                {
                    child.Remove();
                    Save();
                    return 1;
                }
            }

            return 0;
        }

        public int AddUser(SiteUser user)
        {
            JObject p = JObject.FromObject(user);
            _db.SelectToken("Users").Append(p);
            Save();

            return 1;
        }

        public Role GetRole(int _id)
        {
            foreach (JToken child in _db.SelectToken("Roles").Children())
            {
                JObject JRole = JObject.Parse(child.ToString());

                int id = Int32.Parse(JRole.GetValue("Id").ToString());
                if (id == _id)
                {
                    string name = JRole.GetValue("Name").ToString();

                    Role role = new Role
                    {
                        Id = id,
                        Name = name
                    };

                    return role;
                }
            }

            return null;
        }

        public SiteUser GetUserByName(string _name)
        {
            foreach (JToken child in _db.SelectToken("Users").Children())
            {
                JObject JUser = JObject.Parse(child.ToString());

                string username = JUser.GetValue("Username").ToString();
                if (_name.Equals(username))
                {
                    int id = Int32.Parse(JUser.GetValue("Id").ToString());
                    string password = JUser.GetValue("Password").ToString();
                    int roleId = Int32.Parse(JUser.GetValue("roleId").ToString());

                    SiteUser user = new SiteUser
                    {
                        Id = id,
                        Username = username,
                        Password = password,
                        RoleId = roleId,
                        Role = GetRole(roleId)
                    };

                    return user;
                }
            }

            return null;
        }

        public IList<Category> GetProductCategories(Product product)
        {
            List<Category> categories = new List<Category>();

            foreach (JToken child in _db.SelectToken("CategoryProduct").Children())
            {
                JObject JCP = JObject.Parse(child.ToString());

                int pid = Int32.Parse(JCP.GetValue("ProductId").ToString());
                if (pid.Equals(product.Id))
                {
                    int id = Int32.Parse(JCP.GetValue("CategoryId").ToString());
                    Category category = GetCategory(id);
                    categories.Add(category);
                }
            }

            return categories;
        }

        public IList<Product> GetCategoryProducts(Category category)
        {
            List<Product> products = new List<Product>();

            foreach (JToken child in _db.SelectToken("CategoryProduct").Children())
            {
                JObject JCP = JObject.Parse(child.ToString());

                int pid = Int32.Parse(JCP.GetValue("CategoryId").ToString());
                if (pid.Equals(category.Id))
                {
                    int id = Int32.Parse(JCP.GetValue("ProductId").ToString());
                    Product product = GetProduct(id);
                    products.Add(product);
                }
            }

            return products;
        }

        public int LinkProduct(int productId, int categoryId)
        {
            CategoryProduct cp = new CategoryProduct
            {
                ProductId = productId,
                CategoryId = categoryId
            };

            JObject p = JObject.FromObject(cp);
            _db.SelectToken("CategoryProduct").Append(p);
            Save();

            return 1;
        }

        public int UnlinkProduct(int productId)
        {
            int affected = 0;

            foreach (JToken child in _db.SelectToken("CategoryProduct").Children())
            {
                JObject JCP = JObject.Parse(child.ToString());

                int pid = Int32.Parse(JCP.GetValue("ProductId").ToString());
                if (pid == productId)
                {
                    child.Remove();
                    affected++;
                }
            }
            Save();

            return affected;
        }

        public int UnlinkProduct(int productId, int categoryId)
        {
            int affected = 0;

            foreach (JToken child in _db.SelectToken("CategoryProduct").Children())
            {
                JObject JCP = JObject.Parse(child.ToString());

                int pid = Int32.Parse(JCP.GetValue("ProductId").ToString());
                int cid = Int32.Parse(JCP.GetValue("CategoryId").ToString());
                if (pid == productId && cid == categoryId)
                {
                    child.Remove();
                    affected++;
                }
            }
            Save();

            return affected;
        }

        public int UnlinkCategory(int categoryId)
        {
            int affected = 0;

            foreach (JToken child in _db.SelectToken("CategoryProduct").Children())
            {
                JObject JCP = JObject.Parse(child.ToString());

                int cid = Int32.Parse(JCP.GetValue("CategoryId").ToString());
                if (cid == categoryId)
                {
                    child.Remove();
                    affected++;
                }
            }
            Save();

            return affected;
        }

        public List<SiteUser> UserList()
        {
            List<SiteUser> users = new List<SiteUser>();

            foreach (JToken child in _db.SelectToken("Users").Children())
            {
                JObject JProduct = JObject.Parse(child.ToString());

                int id = Int32.Parse(JProduct.GetValue("Id").ToString());
                string username = JProduct.GetValue("Username").ToString();
                string password = JProduct.GetValue("Password").ToString();
                int roleId = Int32.Parse(JProduct.GetValue("RoleId").ToString());

                SiteUser User = new SiteUser
                {
                    Id = id,
                    Username = username,
                    Password = password,
                    RoleId = roleId
                };

                users.Add(User);
            }

            return users;
        }

        public List<Role> RoleList()
        {
            List<Role> roles = new List<Role>();

            foreach (JToken child in _db.SelectToken("Roles").Children())
            {
                JObject JProduct = JObject.Parse(child.ToString());

                int id = Int32.Parse(JProduct.GetValue("Id").ToString());
                string name = JProduct.GetValue("Name").ToString();

                Role Role = new Role
                {
                    Id = id,
                    Name = name
                };

                roles.Add(Role);
            }

            return roles;
        }

        private int NewId(string token)
        {
            JToken last = _db.SelectToken(token).Children().Last();
            JObject Jlast = JObject.Parse(last.ToString());
            return Int32.Parse(Jlast.GetValue("Id").ToString());
        }
    }
}
