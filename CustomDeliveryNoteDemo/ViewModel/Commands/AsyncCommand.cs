﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.Interfaces;
using ViewModel.Util;

namespace ViewModel.Commands
{
    public class AsyncCommand : IAsyncCommand
    {
        #region Declaration

        public event EventHandler CanExecuteChanged;

        private bool IsExecutingTask;
        private readonly Func<Task> ExecuteTask;
        private readonly Func<bool> CanExecuteTask;
        private readonly IErrorHandler ErrorHandler;

        #endregion

        #region Ctors

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null, IErrorHandler errorHandler = null)
        {
            this.ExecuteTask = execute;
            this.CanExecuteTask = canExecute;
            this.ErrorHandler = errorHandler;
        }

        #endregion

        #region Methods

        public bool CanExecute()
        {
            return !this.IsExecutingTask && (this.CanExecuteTask?.Invoke() ?? true);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Tasks

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    this.IsExecutingTask = true;
                    await ExecuteTask();
                }
                finally
                {
                    this.IsExecutingTask = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        #endregion

        #region ExplicitImplementations

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().FireAndForgetSafeAsync(this.ErrorHandler);
        }

        #endregion
    }
}
