using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /*protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            MainWindowViewModel _vm = (MainWindowViewModel)DataContext;
            //_vm.ViewModelHelper = new ViewModelHelper();
        }*/
    }
}
