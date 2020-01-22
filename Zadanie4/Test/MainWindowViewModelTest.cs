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
        public void AddAndDeleteDepartmentTest()
        {
            MainWindowViewModel _vm = new MainWindowViewModel();
            int databaseSize = _vm.DepartmentRepository.GetAllDepartments().ToList().Count;
            _vm.Name = "_test";
            _vm.GroupName = "_test2";
            _vm.ModifiedDate = DateTime.Now;
            Console.WriteLine(databaseSize);
            _vm.AddDepartmentCommand.Execute(null);
            Console.WriteLine(_vm.DepartmentRepository.GetAllDepartments().ToList().Count);
            Assert.AreEqual(databaseSize + 1, _vm.DepartmentRepository.GetAllDepartments().ToList().Count);

            _vm.Department = _vm.DepartmentRepository.GetDepartmentByName("_test");
            _vm.DeleteDepartmentCommand.Execute(null);
            Assert.AreEqual(databaseSize, _vm.DepartmentRepository.GetAllDepartments().ToList().Count);
        }
        [TestMethod]
        public void UpdateDepartmentTest()
        {
           /* MainWindowViewModel _vm = new MainWindowViewModel();
            _vm.Name = "_test";
            _vm.GroupName = "_test2";
            _vm.ModifiedDate = DateTime.Now;
            _vm.AddDepartmentCommand.Execute(null);

            UpdateWindowViewModel _vm2 = new UpdateWindowViewModel();
            _vm2.Department = _vm.DepartmentRepository.GetDepartmentByName("_test");
            _vm2.Name = "_test";
            _vm2.GroupName = "_test2Edit";
            _vm2.ModifiedDate = DateTime.Now;
            _vm2.Execute(null); //Tu będzie linijka z commend update
            Assert.AreEqual("_test2Edit", _vm.DepartmentRepository.GetDepartmentByName("_test").GroupName);

            _vm.Department = _vm.DepartmentRepository.GetDepartmentByName("_test");
            _vm.DeleteDepartmentCommand.Execute(null);*/
        }
        [TestMethod]
        public void MyCommandTest()
        {
            MainWindowViewModel _vm = new MainWindowViewModel();
            int _boxShowCount = 0;
            _vm.MessageBoxShowDelegate = (messageBoxText) =>
            {
                _boxShowCount++;
                //Assert.AreEqual<string>("ActionText", messageBoxText);
                //Console.WriteLine(messageBoxText);
            };
            _vm.ShowPopupWindow("");
            Assert.AreEqual<int>(1, _boxShowCount);
        }
    }
}
