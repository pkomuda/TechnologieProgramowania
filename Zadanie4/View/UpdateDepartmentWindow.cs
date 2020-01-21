using ViewModel;

namespace View
{
    class UpdateDepartmentWindow : IWindow
    {
        public event VoidHandler OnClose;
        private UpdateDepartmentWindow _window;
        public UpdateDepartmentWindow()
        {
            _window = new UpdateDepartmentWindow();
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
