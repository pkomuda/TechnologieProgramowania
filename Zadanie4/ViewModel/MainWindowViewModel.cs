using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using Data;
using Service;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DepartmentRepository m_DepartmentRepository;
        public DepartmentRepository DepartmentRepository
        {
            get { return m_DepartmentRepository; }
            set
            {
                m_DepartmentRepository = value;
                Departments = new ObservableCollection<Department>(value.GetAllDepartments());
            }
        }

        private ObservableCollection<Department> m_Departments;
        public ObservableCollection<Department> Departments
        {
            get { return m_Departments; }
            set
            {
                m_Departments = value;
                RaisePropertyChanged();
            }
        }

        private Department m_Department;
        public Department Department
        {
            get { return m_Department; }
            set
            {
                m_Department = value;
                RaisePropertyChanged();
            }
        }

        private string m_Name;
        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                RaisePropertyChanged();
            }
        }

        private string m_GroupName;
        public string GroupName
        {
            get { return m_GroupName; }
            set
            {
                m_GroupName = value;
                RaisePropertyChanged();
            }
        }

        private DateTime m_ModifiedDate;
        public DateTime ModifiedDate
        {
            get { return m_ModifiedDate; }
            set
            {
                m_ModifiedDate = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddDepartmentCommand { get; private set; }
        public ICommand DeleteDepartmentCommand { get; private set; }

        public MainWindowViewModel()
        {
            DepartmentRepository = new DepartmentRepository();
            ModifiedDate = DateTime.Now;
            AddDepartmentCommand = new RelayCommand(AddDepartment);
            DeleteDepartmentCommand = new RelayCommand(DeleteDepartment);
        }

        public void AddDepartment()
        {
            Department department = new Department
            {
                Name = Name,
                GroupName = GroupName,
                ModifiedDate = ModifiedDate
            };
            m_DepartmentRepository.AddDepartment(department);
            Name = "";
            GroupName = "";
            ModifiedDate = DateTime.Now;
            Departments = new ObservableCollection<Department>(DepartmentRepository.GetAllDepartments());
        }
        
        public void DeleteDepartment()
        {
            m_DepartmentRepository.DeleteDepartmentByID(Department.DepartmentID);
            Departments = new ObservableCollection<Department>(DepartmentRepository.GetAllDepartments());
        }
    }
}
