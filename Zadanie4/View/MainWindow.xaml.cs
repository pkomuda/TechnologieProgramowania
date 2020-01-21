
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

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
        /*protected override void OnInitialized(EventArgs e)
{
   base.OnInitialized(e);
   MainWindowViewModel _vm = (MainWindowViewModel)DataContext;
   //_vm.ViewModelHelper = new ViewModelHelper();
}*/
    }
}
