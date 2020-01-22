using System.Windows;
using System;
using ViewModel;

namespace View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            MainWindowViewModel mwvm = (MainWindowViewModel) DataContext;
            //mwvm.MessageBoxShowDelegate = text => MessageBox.Show(text, "Button interaction", MessageBoxButton.OK, MessageBoxImage.Information);
            mwvm.WindowResolver = new UpdateDepartmentWindowResolver();
            mwvm.MessageBoxShowDelegate = text => MessageBox.Show(text, "Button interaction", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
