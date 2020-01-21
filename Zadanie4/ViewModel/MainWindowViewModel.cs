using Data;
using Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ProductRepository ProductRepository = new ProductRepository();
        public List<Product> Products
         {
             get
             {
                 return ProductRepository.GetAllProducts().ToList();
             }
         }
        public MainWindowViewModel()
        {
            AddProductCommand = new RelayCommand(AddProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
        }
        private Product m_Product;
        public Product Product
        {
            get
            {
                return m_Product;
            }
            set
            {
                m_Product = value;
                //RaisePropertyChanged();
            }
        }
        /*
        private ObservableCollection<Product> m_Products;
        public ObservableCollection<Product> Products
        {
            get { return m_Products; }
            set
            {
                m_Products = value;
                //RaisePropertyChanged();
            }
        }
        private ProductRepository m_ProductRepository;
        public ProductRepository ProductRepository
        {
            get { return m_ProductRepository; }
            set
            {
                m_ProductRepository = value;

                Task.Run(() =>
                {
                    Products = new ObservableCollection<Product>(value.GetAllProducts());
                });
            }
        }
        */
        public string Name { get; set; }
        public void AddProduct()
        {
            Product product = new Product
            {
                Name = Name,
            };

            Task.Run(() =>
            {
                //m_ProductRepository.AddProduct(product);

            });

        }
        public RelayCommand AddProductCommand
        {
            get; private set;
        }

        public ICommand DeleteProductCommand { get; private set; }
        public void DeleteProduct()
        {
            ProductRepository.DeleteProductByID(Product.ProductID);
        }
    }
}
