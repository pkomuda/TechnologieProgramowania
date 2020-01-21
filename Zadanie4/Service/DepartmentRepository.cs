using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Data;

namespace Service
{
    public class DepartmentRepository
    {
        private LINQToSQLDataContext dataContext;

        public DepartmentRepository()
        {
            dataContext = new LINQToSQLDataContext();
        }

        public void AddDepartment(Department department)
        {
            dataContext.GetTable<Department>().InsertOnSubmit(department);
            dataContext.SubmitChanges();
        }

        public Department GetDepartmentByID(short departmentID)
        {
            return dataContext.GetTable<Department>().First(department => department.DepartmentID.Equals(departmentID));
        }

        public Department GetDepartmentByName(string name)
        {
            return dataContext.GetTable<Department>().First(department => department.Name.Equals(name));
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return dataContext.GetTable<Department>();
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
            dataContext.GetTable<Department>().DeleteOnSubmit(temp);
            dataContext.SubmitChanges();
        }

        public void DeleteDepartmentByName(string name)
        {
            Department temp = GetDepartmentByName(name);
            dataContext.GetTable<Department>().DeleteOnSubmit(temp);
            dataContext.SubmitChanges();
        }
    }
}
