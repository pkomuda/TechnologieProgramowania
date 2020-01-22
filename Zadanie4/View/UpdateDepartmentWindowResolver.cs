using System;
using ViewModel;

namespace View
{
    public class UpdateDepartmentWindowResolver : IWindowResolver
    {
        public IWindow GetWindow()
        {
            return new AddDepartmentWindow();
        }
    }
}
