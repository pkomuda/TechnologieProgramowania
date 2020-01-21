using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
using Service;

namespace Test
{
    [TestClass]
    public class DepartmentRepositoryTest
    {
        [TestMethod]
        public void AddDepartmentTest()
        {
            DepartmentRepository departmentRepository = new DepartmentRepository();
            Department department = new Department
            {
                DepartmentID = 98,
                Name = "TestDepartmentName",
                GroupName = "TestGroupName",
                ModifiedDate = DateTime.Now
            };
            int amount = departmentRepository.GetAllDepartments().ToArray().Length;
            departmentRepository.AddDepartment(department);
            Assert.AreEqual(amount + 1, departmentRepository.GetAllDepartments().ToArray().Length);
            departmentRepository.DeleteDepartmentByName("TestDepartmentName");
        }
        
        [TestMethod]
        public void GetDepartmentTest()
        {
            DepartmentRepository departmentRepository = new DepartmentRepository();
            Department department = new Department
            {
                DepartmentID = 98,
                Name = "TestDepartmentName",
                GroupName = "TestGroupName",
                ModifiedDate = DateTime.Now
            };
            string groupName = department.GroupName;
            departmentRepository.AddDepartment(department);
            Assert.AreEqual(groupName, departmentRepository.GetDepartmentByName("TestDepartmentName").GroupName);
            departmentRepository.DeleteDepartmentByName("TestDepartmentName");
        }
        
        [TestMethod]
        public void GetAllDepartmentsTest()
        {
            DepartmentRepository departmentRepository = new DepartmentRepository();
            Department department1 = new Department
            {
                DepartmentID = 98,
                Name = "TestDepartmentName1",
                GroupName = "TestGroupName1",
                ModifiedDate = DateTime.Now
            };
            Department department2 = new Department
            {
                DepartmentID = 99,
                Name = "TestDepartmentName2",
                GroupName = "TestGroupName2",
                ModifiedDate = DateTime.Now
            };
            departmentRepository.AddDepartment(department1);
            departmentRepository.AddDepartment(department2);
            Assert.IsTrue(departmentRepository.GetAllDepartments().ToArray().Length >= 2);
            Assert.AreEqual("TestDepartmentName1", departmentRepository.GetAllDepartments().ToArray()[departmentRepository.GetAllDepartments().ToArray().Length - 2].Name);
            Assert.AreEqual("TestDepartmentName2", departmentRepository.GetAllDepartments().ToArray()[departmentRepository.GetAllDepartments().ToArray().Length - 1].Name);
            departmentRepository.DeleteDepartmentByName("TestDepartmentName1");
            departmentRepository.DeleteDepartmentByName("TestDepartmentName2");
        }
        
        [TestMethod]
        public void UpdateDepartmentTest()
        {
            DepartmentRepository departmentRepository = new DepartmentRepository();
            Department department = new Department
            {
                DepartmentID = 98,
                Name = "TestDepartmentName",
                GroupName = "TestGroupName",
                ModifiedDate = DateTime.Now
            };
            departmentRepository.AddDepartment(department);
            department.Name = "NewDepartmentName";
            departmentRepository.UpdateDepartmentByName("TestDepartmentName", department);
            Assert.AreEqual("TestGroupName", departmentRepository.GetDepartmentByName("TestDepartmentName").GroupName);
            departmentRepository.DeleteDepartmentByName("TestDepartmentName");
        }
        
        [TestMethod]
        public void DeleteDepartmentTest()
        {
            DepartmentRepository departmentRepository = new DepartmentRepository();
            Department department = new Department
            {
                DepartmentID = 98,
                Name = "TestDepartmentName",
                GroupName = "TestGroupName",
                ModifiedDate = DateTime.Now
            };
            departmentRepository.AddDepartment(department);
            int amount = departmentRepository.GetAllDepartments().ToArray().Length;
            departmentRepository.DeleteDepartmentByName("TestDepartmentName");
            Assert.AreEqual(amount - 1, departmentRepository.GetAllDepartments().ToArray().Length);
        }
    }
}
