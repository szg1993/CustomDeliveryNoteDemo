using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModel.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> exec_in, Predicate<object> canExec_in)
        {
            if (exec_in == null)
            {
                throw new Exception("Nincs execute meghatározva!");
            }
            this.execute = exec_in;
            this.canExecute = canExec_in;
        }

        public RelayCommand(Action<object> exec_in) : this(exec_in, null)
        {

        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            else
            {
                return canExecute(parameter);
            }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
