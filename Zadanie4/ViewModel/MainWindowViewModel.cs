using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using Data;
using Service;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private void Refresh()
        {
            Departments = new ObservableCollection<Department>(DepartmentRepository.GetAllDepartments());
        }

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
                if (value != null)
                {
                    Name = value.Name;
                    GroupName = value.GroupName;
                    ModifiedDate = value.ModifiedDate;
                }
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
        public ICommand UpdateDepartmentCommand { get; private set; }
        public ICommand DeleteDepartmentCommand { get; private set; }
        public ICommand AddWindowCommand { get; private set; }
        public ICommand RefreshWindowCommand { get; private set; }

        public MainWindowViewModel()
        {
            DepartmentRepository = new DepartmentRepository();
            ModifiedDate = DateTime.Now;
            UpdateDepartmentCommand = new Command(UpdateDepartment);
            DeleteDepartmentCommand = new Command(DeleteDepartment);
            AddWindowCommand = new Command(AddWindow);
            RefreshWindowCommand = new Command(RefreshWindow);
        }

        public void UpdateDepartment()
        {
            Task.Run(() =>
            {
                Department temp = new Department
                {
                    Name = Name,
                    GroupName = GroupName,
                    ModifiedDate = ModifiedDate
                };
                try
                {
                    DepartmentRepository.UpdateDepartmentByID(Department.DepartmentID, temp);
                    Department = null;
                    Name = "";
                    GroupName = "";
                    ModifiedDate = DateTime.Now;
                    Refresh();
                    ShowPopupWindow("Department updated successfully.");
                }
                catch(Exception e)
                {
                    ShowPopupWindow("Updating department failed.\nERROR: " + e.Message);
                }
            });
        }

        public void DeleteDepartment()
        {
            Task.Run(() =>
            { 
                try
                {
                    DepartmentRepository.DeleteDepartmentByID(Department.DepartmentID);
                    Department = null;
                    Name = "";
                    GroupName = "";
                    ModifiedDate = DateTime.Now;
                    Refresh();
                    ShowPopupWindow("Department deleted successfully.");
                }
                catch(Exception e)
                {
                    ShowPopupWindow("Deleting department failed.\nERROR: " + e.Message);
                }
          });
        }

        public void AddWindow()
        {
            IWindow window = WindowResolver.GetWindow();
            window.BindViewModel(new AddWindowViewModel());
            window.Show();
        }

        public void RefreshWindow()
        {
            Task.Run(() =>
            {
                try
                {
                    Refresh();
                }
                catch (Exception e)
                {
                    ShowPopupWindow("Refresh failed.\nERROR: " + e.Message);
                }
            });
        }

        public Action<string> MessageBoxShowDelegate { get; set; } = x => throw new ArgumentOutOfRangeException($"The delegate {nameof(MessageBoxShowDelegate)} must be assigned by the view layer");
        public void ShowPopupWindow(string text)
        {
            MessageBoxShowDelegate(text);
        }
    }
}
