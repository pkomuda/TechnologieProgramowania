using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using Service;

namespace Test
{
    [TestClass]
    public class ProductRepositoryTest
    {
        private ProductRepository productRepository = new ProductRepository();
        private Product product = new Product
        {
            ProductID = 9876,
            Name = "TestProductName",
            ProductNumber = "TestProductNumber",
            SafetyStockLevel = 1000,
            ReorderPoint = 750,
            StandardCost = 0,
            ListPrice = 0,
            DaysToManufacture = 0,
            SellStartDate = DateTime.Now,
            SellEndDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            rowguid = new Guid()
        };

        [TestMethod]
        public void AddProductTest()
        {
            int amount = productRepository.GetAllProducts().ToArray().Length;
            productRepository.AddProduct(product);
            Assert.AreEqual(amount + 1, productRepository.GetAllProducts().ToArray().Length);
            productRepository.DeleteProductByName("TestProductName");
        }
        
        [TestMethod]
        public void GetProductTest()
        {
            string productNumber = product.ProductNumber;
            productRepository.AddProduct(product);
            Assert.AreEqual(productNumber, productRepository.GetProductByName("TestProductName").ProductNumber);
            productRepository.DeleteProductByName("TestProductName");
        }
        
        [TestMethod]
        public void GetAllProductsTest()
        {
            ProductRepository productRepository = new ProductRepository();
            Assert.IsTrue(productRepository.GetAllProducts().ToArray().Length > 1);
            Assert.IsNotNull(productRepository.GetAllProducts().ToArray()[0].ProductID);
            Assert.IsNotNull(productRepository.GetAllProducts().ToArray()[1].ProductID);
        }
        
        [TestMethod]
        public void UpdateProductTest()
        {
            productRepository.AddProduct(product);
            product.ProductNumber = "NewProductNumber";
            productRepository.UpdateProductByName("TestProductName", product);
            Assert.AreEqual("NewProductNumber", productRepository.GetProductByName("TestProductName").ProductNumber);
            productRepository.DeleteProductByName("TestProductName");
        }
        
        [TestMethod]
        public void DeleteProductTest()
        {
            productRepository.AddProduct(product);
            int amount = productRepository.GetAllProducts().ToArray().Length;
            productRepository.DeleteProductByName("TestProductName");
            Assert.AreEqual(amount - 1, productRepository.GetAllProducts().ToArray().Length);
        }
    }
}
