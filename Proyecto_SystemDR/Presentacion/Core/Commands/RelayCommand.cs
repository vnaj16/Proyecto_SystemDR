using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentacion.Core.Commands
{
    public class RelayCommand : ICommand
    {
        private Action action;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action _action, Func<bool> _canExecute)
        {
            this.action = _action;
            this.canExecute = _canExecute;
        }

        public RelayCommand(Action _action)
        {
            this.action = _action;
            this.canExecute = () => true;
        }

        public bool CanExecute(object parameter)
        {
            bool result = this.canExecute.Invoke();
            return result;
        }

        public void Execute(object parameter)
        {
            this.action.Invoke();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
