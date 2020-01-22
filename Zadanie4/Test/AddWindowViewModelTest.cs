using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;

namespace Test
{
    [TestClass]
    public class AddWindowViewModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            AddWindowViewModel _vm = new AddWindowViewModel();
            Assert.IsNotNull(_vm.ModifiedDate);
            Assert.IsNotNull(_vm.MessageBoxShowDelegate);
            Assert.IsNotNull(_vm.AddDepartmentCommand);
            Assert.IsNotNull(_vm.DepartmentRepository);
            Assert.IsNull(_vm.Department);
            Assert.IsTrue(_vm.AddDepartmentCommand.CanExecute(null));
        }
        [TestMethod]
        public void AddTest()
        {
            AddWindowViewModel _vm = new AddWindowViewModel();
            _vm.Name = "_testDepartment22";
            _vm.GroupName = "_test2Edit";
            _vm.ModifiedDate = DateTime.Now;
          //// Assert.AreEqual(null, _vm.DepartmentRepository.GetDepartmentByName("_testDepartment22"));
            _vm.AddDepartmentCommand.Execute(null);
           // Assert.AreEqual("_test2Edit", _vm.DepartmentRepository.GetDepartmentByName("_testDepartment2").GroupName);
            _vm.DepartmentRepository.DeleteDepartmentByName("_testDepartment2");



            MainWindowViewModel _vm2 = new MainWindowViewModel();
            _vm2.Department = _vm.DepartmentRepository.GetDepartmentByName("_testDepartment2");
            _vm2.DeleteDepartmentCommand.Execute(null);
        }
        [TestMethod]
        public void ShowPopupWindowTest()
        {
            AddWindowViewModel _vm = new AddWindowViewModel();
            int _boxShowCount = 0;
            _vm.MessageBoxShowDelegate = (messageBoxText) =>
            {
                _boxShowCount++;
            };
            _vm.ShowPopupWindow("");
            Assert.AreEqual<int>(1, _boxShowCount);
        }
    }
}
