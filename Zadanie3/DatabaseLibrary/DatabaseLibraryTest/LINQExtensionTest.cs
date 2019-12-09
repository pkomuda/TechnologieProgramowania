using DatabaseLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace DatabaseLibraryTest
{
    [TestClass]
    public class LINQExtensionTest
    {
        [TestMethod]
        public void GetProductsWithNoCategoryDecarativeTest()
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<Product> productTable = db.GetTable<Product>();
                List<Product> allProducts = (from product in productTable
                                        select product).ToList();
                Assert.AreEqual(209, allProducts.GetProductsWithNoCategoryDeclarative().Count);
            }
        }

        [TestMethod]
        public void GetProductsWithNoCategoryImperativeTest()
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<Product> productTable = db.GetTable<Product>();
                List<Product> allProducts = (from product in productTable
                                        select product).ToList();
                Assert.AreEqual(209, allProducts.GetProductsWithNoCategoryImperative().Count);
            }
        }

        [TestMethod]
        public void GetNProductsOnPageDeclarativeTest()
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<Product> productTable = db.GetTable<Product>();
                List<Product> allProducts = (from product in productTable
                                     select product).ToList();
                List<Product> result = allProducts.GetNProductsOnPageDeclarative(3, 2);
                Assert.AreEqual(3, result.Count);
                Assert.AreEqual(4, result.ElementAt(0).ProductID);
                Assert.AreEqual(316, result.ElementAt(1).ProductID);
                Assert.AreEqual(317, result.ElementAt(2).ProductID);
            }
        }

        [TestMethod]
        public void GetNProductsOnPageImperativeTest()
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<Product> productTable = db.GetTable<Product>();
                List<Product> allProducts = (from product in productTable
                                             select product).ToList();
                List<Product> result = allProducts.GetNProductsOnPageImperative(3, 2);
                Assert.AreEqual(3, result.Count);
                Assert.AreEqual(4, result.ElementAt(0).ProductID);
                Assert.AreEqual(316, result.ElementAt(1).ProductID);
                Assert.AreEqual(317, result.ElementAt(2).ProductID);
            }
        }
    }
}
