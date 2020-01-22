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

        public IWindowResolver WindowResolver { get; set; }
        public ICommand AddDepartmentCommand { get; private set; }
        public ICommand DeleteDepartmentCommand { get; private set; }
        public ICommand UpdateWindowCommand { get; private set; }
        public ICommand DisplayTextCommand { get; private set; }

        public MainWindowViewModel()
        {
            DepartmentRepository = new DepartmentRepository();
            ModifiedDate = DateTime.Now;
            AddDepartmentCommand = new RelayCommand(AddDepartment);
            DeleteDepartmentCommand = new RelayCommand(DeleteDepartment);
            UpdateWindowCommand = new RelayCommand(UpdateWindow);
            DisplayTextCommand = new RelayCommand(ShowPopupWindow, () => !string.IsNullOrEmpty("test"));
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
            ShowPopupWindow();
        }

        public void DeleteDepartment()
        {
            m_DepartmentRepository.DeleteDepartmentByID(Department.DepartmentID);
            Departments = new ObservableCollection<Department>(DepartmentRepository.GetAllDepartments());
        }

        public void UpdateWindow()
        {
             IWindow window = WindowResolver.GetWindow();
             window.BindViewModel(new UpdateWindowViewModel(DepartmentRepository, Department));
             window.Show();
        }
        public Action<string> MessageBoxShowDelegate { get; set; } = x => throw new ArgumentOutOfRangeException($"The delegate {nameof(MessageBoxShowDelegate)} must be assigned by the view layer");
        private void ShowPopupWindow()
        {
            MessageBoxShowDelegate("Department was added correctly.");
        }
    }
}
