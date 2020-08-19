﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewModel.Commands;
using ViewModel.Excep;
using ViewModel.ModelViewModel;
using ViewModel.Util;

namespace ViewModel
{
    public sealed class MainViewModel : ViewModelBase
    {
        #region Declaration

        public delegate void MenuItemNotify(string menuItemName);
        public event MenuItemNotify NewMenuItemEvent;

        #endregion

        #region Commands

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

        #endregion

        #region Ctors

        public MainViewModel()
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Create the corresponding view and open it.
        /// </summary>
        /// <param name="param"></param>
        private void OpenMenuItem(object param)
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

        #endregion
    }
}
