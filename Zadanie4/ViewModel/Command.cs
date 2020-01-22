﻿using System;
using System.Windows.Input;

namespace ViewModel
{
    public class Command : ICommand
    {
        public Command(Action execute) : this(execute, null) { }
        public Command(Action execute, Func<bool> canExecute)
        {
            m_Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (m_CanExecute == null)
                return true;
            if (parameter == null)
                return m_CanExecute();
            return m_CanExecute();
        }

        public virtual void Execute(object parameter)
        {
            m_Execute();
        }

        public event EventHandler CanExecuteChanged;

        internal void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private readonly Action m_Execute;
        private readonly Func<bool> m_CanExecute;
    }
}
