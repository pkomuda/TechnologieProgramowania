﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace DatabaseLibrary
{
    public class LinqService
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<Product> products = db.GetTable<Product>();
                List<Product> result = (from product in products
                                        where product.Name.Contains(namePart)
                                        select product).ToList();
                return result;
            }
        }
        public static List<Product> GetProductsByVendorName(string vendorName)
        {
             using (LINQToSQLDataContext db = new LINQToSQLDataContext())
             {
                 Table <ProductVendor> vendors = db.GetTable<ProductVendor>();
                 List<Product> result = (from vendor in vendors
                                         where vendor.Vendor.Name.Equals(vendorName)
                                         select vendor.Product).ToList();
                 return result;
             }
        }
        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<ProductVendor> vendors = db.GetTable<ProductVendor>();
                List<String> result = (from vendor in vendors
                                        where vendor.Vendor.Name.Equals(vendorName)
                                        select vendor.Product.Name).ToList();
                return result;
            }
        }
        public static string GetProductVendorByProductName(string productName)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<ProductVendor> vendors = db.GetTable<ProductVendor>();
                String result = (from vendor in vendors
                                 where vendor.Product.Name.Equals(productName)
                                 select vendor.Vendor.Name).First();
                return result;
            }
        }
        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<Product> products = db.GetTable<Product>();
                List<Product> result = (from product in products
                                        where product.ProductReview.Count == howManyReviews
                                        select product).ToList();
                return result;
            }
        }
        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                List<Product> result = (from product in db.Product
                                        join review in db.ProductReview on product.ProductID equals review.ProductID
                                        orderby review.ReviewDate descending
                                        select product).Take(howManyProducts).ToList();
                return result;
            }
        }
        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<Product> products = db.GetTable<Product>();
                List<Product> result = (from product in products
                                        orderby product.ProductSubcategory.Name.Equals(categoryName)
                                        select product).Take(n).ToList();
                return result;
            }
        }
        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                 Table<Product> products = db.GetTable<Product>(); ;
                 int result = (int) (from product in products
                                     where (product.ProductSubcategory.ProductCategory.ProductCategoryID.Equals(category.ProductCategoryID)
                                     && product.ProductSubcategory.ProductCategory.Name.Equals(category.Name))
                                     select product.StandardCost).ToList().Sum();
                 return result;
            }
        }

    }
}
