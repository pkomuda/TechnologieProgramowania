using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;

namespace Test
{
    [TestClass]
    public class MainWindowViewModelTest
    {
        [TestMethod]
        public void ConstructorTestMethod()
        {
            MainWindowViewModel _vm = new MainWindowViewModel();
            Assert.IsNotNull(_vm.ModifiedDate);
            //Assert.IsNotNull(_vm.MessageBoxShowDelegate);
            Assert.IsNotNull(_vm.AddDepartmentCommand);
            Assert.IsNotNull(_vm.DeleteDepartmentCommand);
            Assert.IsNotNull(_vm.UpdateWindowCommand);
            Assert.IsNotNull(_vm.DepartmentRepository);
            Assert.IsNull(_vm.Department);
            Assert.IsNotNull(_vm.Departments);
            Assert.IsTrue(_vm.AddDepartmentCommand.CanExecute(null));
            Assert.IsTrue(_vm.DeleteDepartmentCommand.CanExecute(null));
            Assert.IsTrue(_vm.UpdateWindowCommand.CanExecute(null));
        }
    }
}
