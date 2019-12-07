using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    public class LinqService
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            return null;
        }
        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            return null;
        }
        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            return null;
        }
        public static string GetProductVendorByProductName(string productName)
        {
            return null;
        }
        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            return null;
        }
        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            return null;
        }
        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            return null;
        }
        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            return 0;
        }

    }
}
