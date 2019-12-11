using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace DatabaseLibrary
{
    public static class LINQExtension
    {
        public static List<Product> GetProductsWithNoCategoryDeclarative(this List<Product> products)
        {
            return (from product in products
                    where product.ProductSubcategoryID.Equals(null)
                    select product).ToList();
        }

        public static List<Product> GetProductsWithNoCategoryImperative(this List<Product> products)
        {
            return products.Where(product => product.ProductSubcategoryID.Equals(null)).ToList();
        }

        public static List<Product> GetNProductsOnPageDeclarative(this List<Product> products, int pageSize, int pageNumber)
        {
            return (from product in products
                    select product).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        public static List<Product> GetNProductsOnPageImperative(this List<Product> products, int pageSize, int pageNumber)
        {
            return products.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        public static string GetProductAndVendorNames(this List<Product> products)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<ProductVendor> productVendorTable = db.GetTable<ProductVendor>();
                List<ProductVendor> productVendorAll = (from productVendor in productVendorTable
                                                        select productVendor).ToList();

                var query = from p in products
                            from pv in productVendorAll
                            where p.ProductID == pv.ProductID
                            select new
                            {
                                ProductName = p.Name,
                                VendorName = pv.Vendor.Name
                            };

                return String.Join(Environment.NewLine, (from q in query
                                                         select q.ProductName + " - " + q.VendorName).ToList());
            }
        }
    }
}
