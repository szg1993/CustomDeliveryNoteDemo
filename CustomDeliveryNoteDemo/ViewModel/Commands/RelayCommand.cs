using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ViewModel.Commands
{
    public class RelayCommand : ICommand
    {
        #region Declaration

        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Ctors

        public RelayCommand(Action<object> exec_in, Predicate<object> canExec_in)
        {
            this.execute = exec_in ?? throw new Exception("There is no execute parameter!");
            this.canExecute = canExec_in;
        }

        public RelayCommand(Action<object> exec_in) : this(exec_in, null)
        {

        }

        #endregion

        #region Methods

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

        #endregion
    }
}
