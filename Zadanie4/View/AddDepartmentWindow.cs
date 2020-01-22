using ViewModel;

namespace View
{
    class AddDepartmentWindow : IWindow
    {
        private UpdateWindow _window;

        public AddDepartmentWindow()
        {
            _window = new UpdateWindow();
        }

        public void Show()
        {
            _window.Show();
        }
    }
}
