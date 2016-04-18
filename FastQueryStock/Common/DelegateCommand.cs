using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastQueryStock.Common
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> executeAction;
        private readonly Func<T, bool> canExecuteAction;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> executeAction)
            : this(executeAction, null)
        {
        }

        public DelegateCommand(Action<T> executeAction,
            Func<T, bool> canExecuteAction)        
        {
            this.executeAction = executeAction;
            this.canExecuteAction = canExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteAction != null)
            {
                return canExecuteAction((T)parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                executeAction((T)parameter);
            }
        }
    }

    public class DelegateCommand : ICommand
    {
        private readonly Action executeAction;
        private readonly Func<bool> canExecuteAction;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action executeAction)
            : this(executeAction, null)
        {
        }

        public DelegateCommand(Action executeAction,
            Func<bool> canExecuteAction)
        {
            this.executeAction = executeAction;
            this.canExecuteAction = canExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteAction != null)
            {
                return canExecuteAction();
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                executeAction();
            }
        }
    }
}
