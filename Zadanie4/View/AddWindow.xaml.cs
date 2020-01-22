using System;
using System.Windows;
using ViewModel;

namespace View
{
    public partial class UpdateWindow : Window
    {
        public UpdateWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            AddWindowViewModel mwvm = (AddWindowViewModel) DataContext;
            mwvm.WindowResolver = new UpdateDepartmentWindowResolver();
            mwvm.MessageBoxShowDelegate = text => MessageBox.Show(text, "Button interaction", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
