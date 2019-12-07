using DatabaseLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DatabaseLibraryTest
{
    [TestClass]
    public class LinqServiceTest
    {
        [TestMethod]
        public void GetProductsByNameTest()
        {
            List<Product> products = LinqService.GetProductsByName("Gloves");
            Assert.AreEqual(6, products.Count);
            foreach (Product product in products)
                Assert.IsTrue(product.Name.Contains("Gloves"));
        }
        [TestMethod]
        public void GetProductsByVendorNameTest()
        {
            List<Product> products = LinqService.GetProductsByVendorName("Bergeron Off-Roads");
            Assert.AreEqual(24, products.Count);
        }
        [TestMethod]
        public void GetProductNamesByVendorNameTest()
        {
            List<string> names = LinqService.GetProductNamesByVendorName("Bergeron Off-Roads");
            Assert.AreEqual(24, names.Count);
        }
        [TestMethod]
        public void GetProductVendorByProductNameTest()
        {
            string name = LinqService.GetProductVendorByProductName("Hex Nut 5");
            Assert.AreEqual("Cruger Bike Company", name);
        }
        [TestMethod]
        public void GetProductsWithNRecentReviewsTest()
        {
            List<Product> products = LinqService.GetProductsWithNRecentReviews(2);
            Assert.AreEqual(1, products.Count);

        }
        [TestMethod]
        public void GetNRecentlyReviewedProductsTest()
        {
            List<Product> products = LinqService.GetNRecentlyReviewedProducts(5);
            Assert.AreEqual(products.Count, 4);
            Assert.AreEqual("HL Mountain Pedal", products[0].Name);
            Assert.AreEqual("Road-550-W Yellow, 40", products[1].Name);
            Assert.AreEqual("HL Mountain Pedal", products[2].Name);
            Assert.AreEqual("Mountain Bike Socks, M", products[3].Name);
        }
        [TestMethod]
        public void GetNProductsFromCategoryTest()
        {
            List<Product> products = LinqService.GetNProductsFromCategory("Headsets", 3);
            Assert.AreEqual(3, products.Count);
        }
        [TestMethod]
        public void GetTotalStandardCostByCategoryTest()
        {
            ProductCategory category = new ProductCategory();
            category.Name = "Bikes";
            int total = LinqService.GetTotalStandardCostByCategory(category);
            System.Console.WriteLine(total); 
           // Assert.AreEqual(total, 92092);
        }
    }
}
