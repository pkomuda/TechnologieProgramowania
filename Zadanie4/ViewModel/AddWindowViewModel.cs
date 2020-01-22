using Data;
using Service;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class AddWindowViewModel : ViewModelBase
    {
        public DepartmentRepository DepartmentRepository { get; set; }

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
        public ICommand ListCommand { get; private set; }

        public AddWindowViewModel()
        {
            DepartmentRepository = new DepartmentRepository();
            ModifiedDate = DateTime.Now;
            AddDepartmentCommand = new Command(AddDepartment);
        }
        
        public void AddDepartment()
        {
            Task.Run(() =>
            {
                try
                {
                    Department temp = new Department
                    {
                        Name = Name,
                        GroupName = GroupName,
                        ModifiedDate = ModifiedDate
                    };
                    DepartmentRepository.AddDepartment(temp);
                    Name = "";
                    GroupName = "";
                    ModifiedDate = DateTime.Now;
                    ShowPopupWindow("Department added successfully.");
                }
                catch (Exception e)
                {
                    ShowPopupWindow("Adding department failed.\nERROR: " + e.Message);
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
