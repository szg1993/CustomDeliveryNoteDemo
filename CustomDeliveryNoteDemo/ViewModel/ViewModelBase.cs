using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls.Primitives;
using ViewModel.Commands;
using ViewModel.Excep;
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

        public delegate void MenuItemNotify(string menuItemName);
        public event MenuItemNotify NewMenuItemEvent;

        public delegate void ComboBoxNotify();
        public event ComboBoxNotify ComboBoxEvent;

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

        private RelayCommand openMenuItemCommand;

        public RelayCommand OpenMenuItemCommand
        {
            get
            {
                if (this.openMenuItemCommand == null)
                {
                    this.openMenuItemCommand = new RelayCommand(OpenMenuItem);
                }

                return openMenuItemCommand;
            }
        }

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

        /// <summary>
        /// Create the corresponding view and open it.
        /// </summary>
        /// <param name="param"></param>
        public void OpenMenuItem(object param)
        {
            try
            {
                OnCursorHandling(true);

                if (param != null)
                {
                    string menuItemName = param is object[]? Convert.ToString((param as object[])[0]) : Convert.ToString(param);
                    if (String.IsNullOrEmpty(menuItemName))
                    {
                        throw new MessageException("There is no class attached to the menu item.");
                    }

                    NewMenuItemEvent.Invoke(menuItemName);
                }
                else
                {
                    return;
                }
            }
            catch (MessageException mex)
            {
                OnMessageBoxHandling(mex.Message, DeliveryNoteMessageBoxType.Warning);
            }
            catch (Exception ex)
            {
                OnMessageBoxHandling(ex.Message, DeliveryNoteMessageBoxType.Error);
            }
            finally
            {
                OnCursorHandling(false);
            }
        }

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

        protected virtual void OnComboBoxHandling()
        {
            ComboBoxEvent.Invoke();
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
