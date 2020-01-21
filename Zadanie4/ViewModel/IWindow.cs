namespace ViewModel
{
    public interface IWindow
    {
        void BindViewModel<T>(T viewModel) where T : ViewModelBase;
        void Show();
        event VoidHandler OnClose;
    }

    public delegate void VoidHandler();
}