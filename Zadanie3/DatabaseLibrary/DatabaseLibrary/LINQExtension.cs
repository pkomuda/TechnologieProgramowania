using System.Collections.Generic;
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
    }
}
