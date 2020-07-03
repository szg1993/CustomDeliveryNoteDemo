using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region Declaration

        /// <summary>
        /// Needed because of the dispose pattern.
        /// </summary>
        private bool isDisposed;

        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void MouseNotify(bool isWaiting);
        public event MouseNotify MouseEvent;

        public delegate void MsgNotify(string msg);
        public event MsgNotify MessageBoxEvent;

        #endregion

        #region Methods

        protected virtual void OnCursorHandling(bool isWaiting)
        {
            MouseEvent.Invoke(isWaiting);
        }

        protected virtual void OnMessageBoxHandling(string msg)
        {
            MessageBoxEvent.Invoke(msg);
        }


        /// <summary>
        /// Check if the property is exists.
        /// </summary>
        /// <param name="propertyName_in"></param>
        protected void VerifyPropertyName(string propertyName_in)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName_in] == null)
            {
                throw new Exception(String.Format("Nincs ilyen nevű tulajdonság: {0} !", propertyName_in));
            }
        }

        /// <summary>
        /// Inform the UI if something changed.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            isDisposed = true;
        }

        #endregion
    }
}
