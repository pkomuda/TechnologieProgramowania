using DatabaseLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace DatabaseLibraryTest
{
    [TestClass]
    public class MyProductServiceTest
    {
        private static void FillMyProducts(List<MyProduct> myProducts)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<Product> productTable = db.GetTable<Product>();
                List<Product> products = (from product in productTable
                                          select product).Take(100).ToList();
                string[] countries = { "Poland", "Germany", "USA" };
                Random random = new Random();
                foreach (Product p in products)
                {
                    myProducts.Add(new MyProduct(p, countries[random.Next(0, countries.Length)]));
                }
            }
        }

        [TestMethod]
        public void GetMyProductsByNameTest()
        {
            List<MyProduct> myProductsAll = new List<MyProduct>();
            FillMyProducts(myProductsAll);

            List<MyProduct> myProducts = myProductsAll.GetMyProductsByName("Ball");
            Assert.AreEqual(3, myProducts.Count);
            foreach (MyProduct myProduct in myProducts)
            {
                Console.WriteLine(myProduct.Name);
                Assert.IsTrue(myProduct.Name.Contains("Ball"));
                Assert.IsTrue(myProduct.CountryOfOrigin == "Poland"
                    || myProduct.CountryOfOrigin == "Germany"
                    || myProduct.CountryOfOrigin == "USA");
            }
        }

        [TestMethod]
        public void GetMyProductsByVendorNameTest()
        {
            List<MyProduct> myProductsAll = new List<MyProduct>();
            FillMyProducts(myProductsAll);

            List<MyProduct> myProducts = myProductsAll.GetMyProductsByVendorName("Training Systems");
            Assert.AreEqual(3, myProducts.Count);
        }

        [TestMethod]
        public void GetMyProductsBy()
        {
            List<MyProduct> myProductsAll = new List<MyProduct>();
            FillMyProducts(myProductsAll);

            List<MyProduct> myProducts = myProductsAll.GetMyProductsWithNRecentReviews(0);
            Assert.AreEqual(100, myProducts.Count);
        }
    }
}
