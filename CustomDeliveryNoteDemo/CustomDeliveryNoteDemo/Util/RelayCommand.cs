using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CustomDeliveryNoteDemo.Util
{
    public class RelayCommand : ICommand
    {
        #region Declaration

        private Action<object> execute;
        private Predicate<object> canExecute;

        #endregion

        #region Ctors

        public RelayCommand(Action<object> execIn, Predicate<object> canExecIn)
        {
            this.execute = execIn ?? throw new Exception("The execute isn't declared");
            this.canExecute = canExecIn;
        }

        public RelayCommand(Action<object> execIn) : this(execIn, null)
        {

        }

        #endregion

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

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
