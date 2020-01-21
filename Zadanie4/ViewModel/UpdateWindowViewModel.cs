﻿using Data;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UpdateWindowViewModel : ViewModelBase
    {
        private DepartmentRepository m_DepartmentRepository;
        public DepartmentRepository DepartmentRepository
        {
            get { return m_DepartmentRepository; }
            set
            {
                m_DepartmentRepository = value;
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

        public UpdateWindowViewModel(DepartmentRepository departmentRepository, Department department)
        {
            DepartmentRepository = departmentRepository;
            Department = department;
        }
        public UpdateWindowViewModel()
        {
        }
    }
}