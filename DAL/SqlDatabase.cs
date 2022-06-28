using Projekt.NET.DAL;
using Projekt.NET.Models;
using System.Data;
using System.Data.SqlClient;

namespace Projekt.NET.DAL
{
    public class SqlDatabase : IDatabase
    {
        public IConfiguration _configuration { get; set; }
        SqlConnection con { get; set; }

        public SqlDatabase(IConfiguration _configuration)
        {
            this._configuration = _configuration;

            String connectionString = _configuration.GetConnectionString("ProjektNETContext");
            con = new SqlConnection(connectionString);
        }


        public int AddProduct(Product _product)
        {
            SqlCommand cmd = new SqlCommand("createProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.NVarChar, -1);
            nameParam.Value = _product.Name;
            cmd.Parameters.Add(nameParam);

            SqlParameter descriptionParam = new SqlParameter("@description", SqlDbType.NVarChar, -1);
            descriptionParam.Value = _product.Description;
            cmd.Parameters.Add(descriptionParam);

            SqlParameter imgPathParam = new SqlParameter("@imgPath", SqlDbType.NVarChar, -1);
            imgPathParam.Value = _product.ImgPath;
            cmd.Parameters.Add(imgPathParam);

            SqlParameter priceParam = new SqlParameter("@price", SqlDbType.Money, 0);
            priceParam.Value = _product.Price;
            cmd.Parameters.Add(priceParam);

            SqlParameter outputParam = new SqlParameter("@id", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputParam);


            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            int id = (int)cmd.Parameters["@id"].Value;

            return id;
        }

        public int DeleteProduct(int _id)
        {
            SqlCommand cmd = new SqlCommand("deleteProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idParam = new SqlParameter("@productID", SqlDbType.Int);
            idParam.Value = _id;
            cmd.Parameters.Add(idParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }

        public Product GetProduct(int _id)
        {
            Product product = new Product();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE Id=" + _id, con);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string description = reader.GetString(2);
                string imgPath = reader.GetString(3);
                decimal price = reader.GetDecimal(4);

                product = new Product
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    ImgPath = imgPath,
                    Price = price
                };
            }
            reader.Close();
            con.Close();

            return product;
        }

        public List<Product> ProductList()
        {
            List<Product> products = new List<Product>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Product", con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string description = reader.GetString(2);
                string imgPath = reader.GetString(3);
                decimal price = reader.GetDecimal(4);

                Product product = new Product
                {
                    Id = id,
                    Name = name,
                    Description= description,
                    ImgPath= imgPath,
                    Price = price
                };
                products.Add(product);
            }
            reader.Close();
            con.Close();

            return products;
        }

        public int UpdateProduct(Product _product)
        {
            SqlCommand cmd = new SqlCommand("updateProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idParam = new SqlParameter("@productId", SqlDbType.Int);
            idParam.Value = _product.Id;
            cmd.Parameters.Add(idParam);

            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.NVarChar, -1);
            nameParam.Value = _product.Name;
            cmd.Parameters.Add(nameParam);

            SqlParameter descriptionParam = new SqlParameter("@description", SqlDbType.NVarChar, -1);
            descriptionParam.Value = _product.Description;
            cmd.Parameters.Add(descriptionParam);

            SqlParameter imgPathParam = new SqlParameter("@imgPath", SqlDbType.NVarChar, -1);
            imgPathParam.Value = _product.ImgPath;
            cmd.Parameters.Add(imgPathParam);

            SqlParameter priceParam = new SqlParameter("@price", SqlDbType.Money, 0);
            priceParam.Value = _product.Price;
            cmd.Parameters.Add(priceParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }








        public int AddCategory(Category _category)
        {
            SqlCommand cmd = new SqlCommand("createCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.NVarChar, -1);
            nameParam.Value = _category.Name;
            cmd.Parameters.Add(nameParam);

            SqlParameter outputParam = new SqlParameter("@id", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            int id = (int)cmd.Parameters["@id"].Value;

            return id;
        }

        public int DeleteCategory(int _id)
        {
            SqlCommand cmd = new SqlCommand("deleteCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idParam = new SqlParameter("@categoryId", SqlDbType.Int);
            idParam.Value = _id;
            cmd.Parameters.Add(idParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }

        public Category GetCategory(int _id)
        {
            Category category = new Category();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Category WHERE Id=" + _id, con);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);

                category = new Category
                {
                    Id = id,
                    Name = name,
                };
            }
            reader.Close();
            con.Close();

            return category;
        }

        public List<Category> CategoryList()
        {
            List<Category> categories = new List<Category>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Category", con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);

                Category product = new Category
                {
                    Id = id,
                    Name = name,
                };
                categories.Add(product);
            }
            reader.Close();
            con.Close();

            return categories;
        }

        public int UpdateCategory(Category _category)
        {
            SqlCommand cmd = new SqlCommand("updateCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idParam = new SqlParameter("@categoryId", SqlDbType.Int);
            idParam.Value = _category.Id;
            cmd.Parameters.Add(idParam);

            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.NVarChar, -1);
            nameParam.Value = _category.Name;
            cmd.Parameters.Add(nameParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }

        public bool ValidateUser(SiteUser user)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE Username=@username AND Password=@password", con);
            
            SqlParameter usernameParam = new SqlParameter("@username", SqlDbType.NVarChar, -1);
            usernameParam.Value = user.Username;
            cmd.Parameters.Add(usernameParam);

            SqlParameter passwordParam = new SqlParameter("@password", SqlDbType.NVarChar, -1);
            passwordParam.Value = user.Password;
            cmd.Parameters.Add(passwordParam);



            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                con.Close();
                return true;
            }
            reader.Close();
            con.Close();

            return false;
        }

        public SiteUser GetUser(int _id)
        {
            SiteUser user = new SiteUser();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE Id=" + _id, con);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string password = reader.GetString(2);
                int roleId = reader.GetInt32(3);

                user = new SiteUser
                {
                    Id = id,
                    Username = name,
                    Password = password,
                    RoleId = roleId,
                };
            }
            reader.Close();
            con.Close();

            return user;
        }

        public int UpdateUser(SiteUser user)
        {
            SqlCommand cmd = new SqlCommand("updateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idParam = new SqlParameter("@userId", SqlDbType.Int);
            idParam.Value = user.Id;
            cmd.Parameters.Add(idParam);

            SqlParameter usernameParam = new SqlParameter("@username", SqlDbType.NVarChar, -1);
            usernameParam.Value = user.Username;
            cmd.Parameters.Add(usernameParam);

            SqlParameter passwordParam = new SqlParameter("@password", SqlDbType.NVarChar, -1);
            passwordParam.Value = user.Password;
            cmd.Parameters.Add(passwordParam);

            SqlParameter roleParam = new SqlParameter("@roleId", SqlDbType.Int);
            roleParam.Value = user.RoleId;
            cmd.Parameters.Add(roleParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }

        public int DeleteUser(int _id)
        {
            SqlCommand cmd = new SqlCommand("deleteUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idParam = new SqlParameter("@userId", SqlDbType.Int);
            idParam.Value = _id;
            cmd.Parameters.Add(idParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }

        public int AddUser(SiteUser user)
        {
            SqlCommand cmd = new SqlCommand("createUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter usernameParam = new SqlParameter("@username", SqlDbType.NVarChar, -1);
            usernameParam.Value = user.Username;
            cmd.Parameters.Add(usernameParam);

            SqlParameter passwordParam = new SqlParameter("@password", SqlDbType.NVarChar, -1);
            passwordParam.Value = user.Password;
            cmd.Parameters.Add(passwordParam);

            SqlParameter roleParam = new SqlParameter("@roleId", SqlDbType.Int);
            roleParam.Value = user.RoleId;
            cmd.Parameters.Add(roleParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }


        public Role GetRole(int _id)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            Role role = new Role();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Role] WHERE Id=" + _id, con);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);

                role = new Role
                {
                    Id = id,
                    Name = name
                };
            }
            reader.Close();
            con.Close();

            return role;
        }

        public SiteUser GetUserByName(string _name)
        {
            SiteUser user = new SiteUser();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE Username=@username", con);

            SqlParameter usernameParam = new SqlParameter("@username", SqlDbType.NVarChar, -1);
            usernameParam.Value = _name;
            cmd.Parameters.Add(usernameParam);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string password = reader.GetString(2);
                int roleId = reader.GetInt32(3);

                user = new SiteUser
                {
                    Id = id,
                    Username = name,
                    Password = password,
                    RoleId = roleId,
                };
            }
            reader.Close();
            con.Close();

            return user;
        }

        public IList<Category> GetProductCategories(Product product)
        {
            int _productId = product.Id;

            List<Category> categories = new List<Category>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Category WHERE Id IN (" +
                "SELECT CategoryId FROM CategoryProduct WHERE ProductId=@productId" +
                ")", con);

            SqlParameter idParam = new SqlParameter("@productId", SqlDbType.Int);
            idParam.Value = _productId;
            cmd.Parameters.Add(idParam);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);

                Category category = new Category
                {
                    Id = id,
                    Name = name,
                };
                categories.Add(category);
            }
            reader.Close();
            con.Close();

            return categories;
        }

        public IList<Product> GetCategoryProducts(Category category)
        {
            int _categoryId = category.Id;

            List<Product> products = new List<Product>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE Id IN (" +
                "SELECT ProductId FROM CategoryProduct WHERE CategoryId=@categoryId" +
                ")", con);

            SqlParameter idParam = new SqlParameter("@categoryId", SqlDbType.Int);
            idParam.Value = _categoryId;
            cmd.Parameters.Add(idParam);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string description = reader.GetString(2);
                string imgPath = reader.GetString(3);
                decimal price = reader.GetDecimal(4);

                Product product = new Product
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    ImgPath = imgPath,
                    Price = price
                };
                products.Add(product);
            }
            reader.Close();
            con.Close();

            return products;
        }

        public int LinkProduct(int productId, int categoryId)
        {
            List<Product> products = new List<Product>();
            SqlCommand cmd = new SqlCommand("INSERT INTO CategoryProduct (ProductId, CategoryId) VALUES(@pid, @cid)", con);

            SqlParameter pidParam = new SqlParameter("@pid", SqlDbType.Int);
            pidParam.Value = productId;
            cmd.Parameters.Add(pidParam);

            SqlParameter cidParam = new SqlParameter("@cid", SqlDbType.Int);
            cidParam.Value = categoryId;
            cmd.Parameters.Add(cidParam);


            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }

        public int UnlinkProduct(int productId)
        {
            List<Product> products = new List<Product>();
            SqlCommand cmd = new SqlCommand("DELETE FROM CategoryProduct WHERE ProductId=@productId", con);

            SqlParameter pidParam = new SqlParameter("@productId", SqlDbType.Int);
            pidParam.Value = productId;
            cmd.Parameters.Add(pidParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }

        public int UnlinkProduct(int productId, int categoryId)
        {
            List<Product> products = new List<Product>();
            SqlCommand cmd = new SqlCommand("DELETE FROM CategoryProduct WHERE ProductId=@productId AND CategoryId=@categoryId", con);

            SqlParameter pidParam = new SqlParameter("@productId", SqlDbType.Int);
            pidParam.Value = productId;
            cmd.Parameters.Add(pidParam);

            SqlParameter cidParam = new SqlParameter("@categoryId", SqlDbType.Int);
            cidParam.Value = categoryId;
            cmd.Parameters.Add(cidParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }

        public List<SiteUser> UserList()
        {
            List<SiteUser> users = new List<SiteUser>();
            
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User]", con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string password = reader.GetString(2);
                int roleId = reader.GetInt32(3);

                SiteUser user = new SiteUser
                {
                    Id = id,
                    Username = name,
                    Password = password,
                    RoleId = roleId,
                };
                users.Add(user);
            }
            reader.Close();
            con.Close();

            return users;
        }

        public List<Role> RoleList()
        {
            List<Role> roles = new List<Role>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM [Role]", con);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);

                Role role = new Role
                {
                    Id = id,
                    Name = name
                };
                roles.Add(role);
            }
            reader.Close();
            con.Close();

            return roles;
        }

        public int UnlinkCategory(int categoryId)
        {
            List<Product> products = new List<Product>();
            SqlCommand cmd = new SqlCommand("DELETE FROM CategoryProduct WHERE CategoryId=@categoryId", con);

            SqlParameter cidParam = new SqlParameter("@categoryId", SqlDbType.Int);
            cidParam.Value = cidParam;
            cmd.Parameters.Add(cidParam);

            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
        }
    }
}
