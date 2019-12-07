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
        public static List<Product> GetProductsByName(string nameContains)
        {
            using (LINQToSQLDataContext dataContext = new LINQToSQLDataContext())
            {
                Table<Product> products = dataContext.GetTable<Product>();
                List<Product> answer = (from product in products
                                        where product.Name.Contains(nameContains)
                                        select product).ToList();
                return answer;
            }
        }
    }
}
