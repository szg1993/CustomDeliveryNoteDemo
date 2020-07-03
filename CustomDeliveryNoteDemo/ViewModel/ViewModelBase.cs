using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace ViewModel
{
    public enum DeliveryNoteMessageBoxType
    {
        ConfirmationWithYesNo,
        ConfirmationWithYesNoCancel,
        Information,
        Error,
        Warning
    }

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

        public delegate void MsgNotify(string msg, DeliveryNoteMessageBoxType type);
        public event MsgNotify MessageBoxEvent;

        #endregion

        #region Methods

        /// <summary>
        /// Begin changing the cursor.
        /// </summary>
        /// <param name="isWaiting"></param>
        protected virtual void OnCursorHandling(bool isWaiting)
        {
            MouseEvent.Invoke(isWaiting);
        }

        /// <summary>
        /// Begin show the messagebox.
        /// </summary>
        /// <param name="msg"></param>
        protected virtual void OnMessageBoxHandling(string msg, DeliveryNoteMessageBoxType type)
        {
            MessageBoxEvent.Invoke(msg, type);
        }


        /// <summary>
        /// Check if the property is exists.
        /// </summary>
        /// <param name="propertyName_in"></param>
        protected void VerifyPropertyName(string propertyName_in)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName_in] == null)
            {
                throw new Exception(String.Format("There is no property with this name: {0} !", propertyName_in));
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
