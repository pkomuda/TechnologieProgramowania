using Data;
using Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

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
            get
            {
                return m_Department;
            }
            set
            {
                m_Department = value;
                RaisePropertyChanged();
            }
        }

        public string Name { get; set; }

        public ICommand AddDepartmentCommand { get; private set; }
        public ICommand DeleteDepartmentCommand { get; private set; }

        public MainWindowViewModel()
        {
            DepartmentRepository = new DepartmentRepository();
            AddDepartmentCommand = new RelayCommand(AddDepartment);
            DeleteDepartmentCommand = new RelayCommand(DeleteDepartment);
        }

        public void AddDepartment()
        {
            Department department = new Department
            {
                Name = Name,
            };
            Task.Run(() =>
            {
                m_DepartmentRepository.AddDepartment(department);
            });
        }
        
        public void DeleteDepartment()
        {
            Task.Run(() =>
            {
                m_DepartmentRepository.DeleteDepartmentByID(Department.DepartmentID);
            });
        }
    }
}
