using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Data;

namespace Service
{
    public class DepartmentRepository
    {
        private LINQToSQLDataContext dataContext;
        private Table<Department> departments;
        private Table<EmployeeDepartmentHistory> employeeDepartments;

        public DepartmentRepository()
        {
            dataContext = new LINQToSQLDataContext();
            departments = dataContext.GetTable<Department>();
            employeeDepartments = dataContext.GetTable<EmployeeDepartmentHistory>();
        }

        public void AddDepartment(Department department)
        {
            departments.InsertOnSubmit(department);
            dataContext.SubmitChanges();
        }

        public Department GetDepartmentByID(short departmentID)
        {
            return departments.First(department => department.DepartmentID.Equals(departmentID));
        }

        public Department GetDepartmentByName(string name)
        {
            return departments.First(department => department.Name.Equals(name));
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return departments;
        }

        public void UpdateDepartmentByID(short departmentID, Department department)
        {
            Department temp = GetDepartmentByID(departmentID);
            foreach (var property in department.GetType().GetProperties())
            {
                property.SetValue(temp, property.GetValue(department));
            }
            temp.DepartmentID = departmentID;
            dataContext.SubmitChanges();
        }

        public void UpdateDepartmentByName(string name, Department department)
        {
            Department temp = GetDepartmentByName(name);
            foreach (var property in department.GetType().GetProperties())
            {
                property.SetValue(temp, property.GetValue(department));
            }
            temp.Name = name;
            dataContext.SubmitChanges();
        }

        public void DeleteDepartmentByID(short departmentID)
        {
            Department temp = GetDepartmentByID(departmentID);
            var query = from e in employeeDepartments
                        where e.DepartmentID == departmentID
                        select e;
            employeeDepartments.DeleteAllOnSubmit(query);
            departments.DeleteOnSubmit(temp);
            dataContext.SubmitChanges();
        }

        public void DeleteDepartmentByName(string name)
        {
            Department temp = GetDepartmentByName(name);
            var query = from e in employeeDepartments
                        join d in departments on e.DepartmentID equals d.DepartmentID
                        where d.Name == name
                        select e;
            employeeDepartments.DeleteAllOnSubmit(query);
            departments.DeleteOnSubmit(temp);
            dataContext.SubmitChanges();
        }
    }
}
