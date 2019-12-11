using DatabaseLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace DatabaseLibraryTest
{
    [TestClass]
    public class MyProductServiceTest
    {
        private List<MyProduct> myProducts = new List<MyProduct>(FillMyProducts());
        private static List<MyProduct> FillMyProducts()
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<Product> productTable = db.GetTable<Product>();
                List<Product> products = (from product in productTable
                                          select product).Take(100).ToList();
                List<MyProduct> myProducts = new List<MyProduct>();
                foreach (Product p in products)
                {
                    myProducts.Add(new MyProduct(p, "Poland"));
                }
                return myProducts;
            }
        }

        [TestMethod]
        public void GetMyProductsByNameTest()
        {
            
        }
    }
}
