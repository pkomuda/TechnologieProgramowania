using System.Collections.Generic;
using System.Linq;
using Data;

namespace Service
{
    public class ProductRepository
    {
        private LINQToSQLDataContext dataContext;

        public ProductRepository()
        {
            dataContext = new LINQToSQLDataContext();
        }

        public void AddProduct(Product product)
        {
            dataContext.GetTable<Product>().InsertOnSubmit(product);
            dataContext.SubmitChanges();
        }

        public Product GetProductByID(int productID)
        {
            return dataContext.GetTable<Product>().First(product => product.ProductID.Equals(productID));
        }

        public Product GetProductByName(string name)
        {
            return dataContext.GetTable<Product>().First(product => product.Name.Equals(name));
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return dataContext.GetTable<Product>();
        }

        public void UpdateProductByID(int productID, Product product)
        {
            Product temp = GetProductByID(productID);
            foreach (var property in product.GetType().GetProperties())
            {
                property.SetValue(temp, property.GetValue(product));
            }
            temp.ProductID = productID;
            dataContext.SubmitChanges();
        }

        public void UpdateProductByName(string name, Product product)
        {
            Product temp = GetProductByName(name);
            foreach (var property in product.GetType().GetProperties())
            {
                property.SetValue(temp, property.GetValue(product));
            }
            temp.Name = name;
            dataContext.SubmitChanges();
        }

        public void DeleteProductByID(int productID)
        {
            Product temp = GetProductByID(productID);
            dataContext.GetTable<Product>().DeleteOnSubmit(temp);
            dataContext.SubmitChanges();
        }

        public void DeleteProductByName(string name)
        {
            Product temp = GetProductByName(name);
            dataContext.GetTable<Product>().DeleteOnSubmit(temp);
            dataContext.SubmitChanges();
        }
    }
}
