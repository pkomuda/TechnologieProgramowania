using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace DatabaseLibrary
{
    public static class MyProductService
    {
        public static List<MyProduct> GetMyProductsByName(this List<MyProduct> myProducts, string namePart)
        {
            return (from myProduct in myProducts
                    where myProduct.Name.Contains(namePart)
                    select myProduct).ToList();
        }

        public static List<MyProduct> GetMyProductsByVendorName(this List<MyProduct> myProducts, string vendorName)
        {
            using (LINQToSQLDataContext db = new LINQToSQLDataContext())
            {
                Table<ProductVendor> vendors = db.GetTable<ProductVendor>();
                return (from myProduct in myProducts
                        join vendor in db.ProductVendor on myProduct.ProductID equals vendor.ProductID
                        where vendor.Vendor.Name == vendorName
                        select myProduct).ToList();
            }
        }

        public static List<MyProduct> GetMyProductsWithNRecentReviews(this List<MyProduct> myProducts, int howManyReviews)
        {
            return (from myProduct in myProducts
                    where myProduct.ProductReview.Count == howManyReviews
                    select myProduct).ToList();
        }
    }
}
