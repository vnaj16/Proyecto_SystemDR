using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentacion.Core.Commands
{
    public class ParamCommand : ICommand
    {
        private Action<object> action;
        private readonly Func<bool> canExecute;

        public ParamCommand(Action<object> _action, Func<bool> _canExecute)
        {
            this.action = _action;
            this.canExecute = _canExecute;
        }

        public ParamCommand(Action<object> _action)
        {
            this.action = _action;
            this.canExecute = () => true;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            else
            {
                bool result = this.canExecute.Invoke();
                return result;
            }
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                if (parameter != null)
                {
                    action(parameter);
                }
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
