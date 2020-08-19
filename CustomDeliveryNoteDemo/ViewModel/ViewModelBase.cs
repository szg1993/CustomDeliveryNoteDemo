using AutoMapper;
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

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Declaration

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
        /// <param name="propertyNameIn"></param>
        protected void VerifyPropertyName(string propertyNameIn)
        {
            if (TypeDescriptor.GetProperties(this)[propertyNameIn] == null)
            {
                throw new Exception(String.Format("There is no property with this name: {0} !", propertyNameIn));
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
    }
}
