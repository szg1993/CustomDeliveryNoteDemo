﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls.Primitives;
using ViewModel.ModelViewModel;

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

        public int LabelFontSize { get; set; } = 12;
        public int LineListLabelFontSize { get; set; } = 10;

        private static MapperConfiguration mapperConfig;

        public static MapperConfiguration MapperConfig
        {
            get { return mapperConfig; }
            set { mapperConfig = value; }
        }

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        #endregion

        #region Ctors

        public ViewModelBase()
        {
            MapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DateTime, long>().ConvertUsing(s => s.Ticks);
                cfg.CreateMap<long, DateTime>().ConvertUsing(s => new DateTime(s));
                cfg.CreateMap<Note, NoteViewModel>();
                cfg.CreateMap<NoteViewModel, Note>();               
                cfg.CreateMap<NoteLine, NoteLineViewModel>();
                cfg.CreateMap<NoteLineViewModel, NoteLine>();
                cfg.CreateMap<Recipient, RecipientViewModel>();
                cfg.CreateMap<RecipientViewModel, Recipient>();
            });
        }

        #endregion

        #region Methods

        public virtual void ReportSuccess()
        {
            OnMessageBoxHandling("The process was successful!", DeliveryNoteMessageBoxType.Information);
        }

        /// <summary>
        /// Begin changing the cursor.
        /// </summary>
        /// <param name="isWaiting"></param>
        protected virtual void OnCursorHandling(bool isWaiting)
        {
            MouseEvent.Invoke(isWaiting);
        }

        /// <summary>
        /// Begin to show the messagebox.
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

        public bool CanExecuteAsyncCommand()
        {
            return !this.IsBusy;
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
                
            }

            isDisposed = true;
        }

        #endregion
    }
}
