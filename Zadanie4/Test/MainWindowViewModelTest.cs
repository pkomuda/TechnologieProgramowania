using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;
using Service;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    [TestClass]
    public class MainWindowViewModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            MainWindowViewModel _vm = new MainWindowViewModel();
            Assert.IsNotNull(_vm.ModifiedDate);
            Assert.IsNotNull(_vm.MessageBoxShowDelegate);
            Assert.IsNotNull(_vm.AddWindowCommand);
            Assert.IsNotNull(_vm.DeleteDepartmentCommand);
            Assert.IsNotNull(_vm.RefreshWindowCommand);
            Assert.IsNotNull(_vm.UpdateDepartmentCommand);
            Assert.IsNotNull(_vm.DepartmentRepository);
            Assert.IsNull(_vm.Department);
            Assert.IsNotNull(_vm.Departments);
            Assert.IsTrue(_vm.RefreshWindowCommand.CanExecute(null));
            Assert.IsTrue(_vm.AddWindowCommand.CanExecute(null));
            Assert.IsTrue(_vm.DeleteDepartmentCommand.CanExecute(null));
            Assert.IsTrue(_vm.UpdateDepartmentCommand.CanExecute(null));
        }
        [TestMethod]
        public void DataRepositoryTest()
        {
            DepartmentRepository repo = new DepartmentRepository();
            MainWindowViewModel _vm = new MainWindowViewModel();
            Assert.IsNotNull(_vm.DepartmentRepository);
            Assert.AreNotSame(_vm.DepartmentRepository, repo);
            _vm.DepartmentRepository = repo;
            Assert.AreSame(_vm.DepartmentRepository, repo);
        }
        [TestMethod]
        public void DeleteDepartmentTest()
        {
            MainWindowViewModel _vm = new MainWindowViewModel();
            int databaseSize = _vm.Departments.ToList().Count;
            _vm.DepartmentRepository.AddDepartment(new Data.Department() { Name = "_testDepartment", GroupName = "_testGroupName", ModifiedDate = DateTime.Now });
            _vm.Department = _vm.DepartmentRepository.GetDepartmentByName("_testDepartment");
            Assert.AreEqual(databaseSize + 1, _vm.DepartmentRepository.GetAllDepartments().ToList().Count);

            _vm.Department = _vm.DepartmentRepository.GetDepartmentByName("_testDepartment");
            _vm.DeleteDepartmentCommand.Execute(null);
            Assert.AreEqual(databaseSize, _vm.Departments.ToList().Count);
        }
        [TestMethod]
        public void UpdateDepartmentTest()
        {
            MainWindowViewModel _vm = new MainWindowViewModel();
            _vm.DepartmentRepository.AddDepartment(new Data.Department() { Name = "_testDepartment2", GroupName = "_testGroupName", ModifiedDate = DateTime.Now });
            _vm.Department = _vm.DepartmentRepository.GetDepartmentByName("_testDepartment2");

            _vm.Name = "_testDepartment2";
            _vm.GroupName = "_test2Edit";
            _vm.ModifiedDate = DateTime.Now;

            _vm.Department = _vm.DepartmentRepository.GetDepartmentByName("_testDepartment2");

            _vm.UpdateDepartmentCommand.Execute(null);

            _vm.DepartmentRepository.DeleteDepartmentByName("_testDepartment2");
        }
        [TestMethod]
        public void ShowPopupWindowTest()
        {
            MainWindowViewModel _vm = new MainWindowViewModel();
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
