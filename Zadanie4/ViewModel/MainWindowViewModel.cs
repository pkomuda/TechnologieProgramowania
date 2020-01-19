using Data;
using Service;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel
{
    public class MainWindowViewModel
    {
        private ProductRepository productRepository = new ProductRepository();
        public List<Product> Products
        {
            get
            {
                return productRepository.GetAllProducts().ToList();
            }
        }
    }
}
