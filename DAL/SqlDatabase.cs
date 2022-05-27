using Projekt.NET.DAL;
using Projekt.NET.Models;
using System.Data;
using System.Data.SqlClient;

namespace ps9.DAL
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


            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
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


            con.Open();
            int affected = cmd.ExecuteNonQuery();
            con.Close();

            return affected;
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
            SqlCommand cmd = new SqlCommand("SELECT * FROM Category WHERE id=" + _id, con);

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
            String connectionString = _configuration.GetConnectionString("ps9");
            SqlConnection con = new SqlConnection(connectionString);

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
    }
}
