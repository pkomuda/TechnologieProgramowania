using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

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

        //skopcone https://stackoverflow.com/questions/22507640/linq-concatenate-values-from-associated-tables
        public static List<string> GetProductAndVendorNames(this List<Product> products)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<ProductVendor> pvTable = db.GetTable<ProductVendor>();
                List<ProductVendor> pvAll = (from productVendor in pvTable
                                             select productVendor).ToList();
                Table<Vendor> vTable = db.GetTable<Vendor>();
                List<Vendor> vAll = (from vendor in vTable
                                     select vendor).ToList();

                var myQuery = from p in products
                              join pv in pvAll on p.ProductID equals pv.ProductID
                              join v in vAll on pv.BusinessEntityID equals v.BusinessEntityID
                              select new
                              {
                                  ProductName = p.Name,
                                  VendorName = v.Name
                              };

                List<string> myResult = from q in myQuery.AsEnumerable()
                                        group q by new { q.ProductName, q.VendorName } into myGroup
                                        select new InnerProductVendor
                                        {
                                            ProductName = myGroup.Key.ProductName,
                                            VendorName = myGroup.Key.VendorName,
                                            ProductVendorName = String.Join(",", values: myGroup.Select(q => q.ProductName), myGroup.Select(q => q.VendorName))
                                        };
                return myResult;
            }
        }

        private class InnerProductVendor
        {
            public string ProductName { get; set; }
            public string VendorName { get; set; }
            public string ProductVendorName { get; set; }
        }
    }
}
