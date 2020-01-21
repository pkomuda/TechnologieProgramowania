using ViewModel;

namespace View
{
    class UpdateDepartmentWindow : IWindow
    {
        public event VoidHandler OnClose;
        private UpdateWindow _window;
        public UpdateDepartmentWindow()
        {
            _window = new UpdateWindow();
        }
        public void BindViewModel<T>(T viewModel) where T : ViewModelBase
        {
            //_window.DataContext = viewModel;
            /*viewModel.CloseWindow = () =>
            {
                OnClose?.Invoke();
                _view.Close();
            };*/
        }

        public void Show()
        {
            _window.Show();
        }
    }
}
