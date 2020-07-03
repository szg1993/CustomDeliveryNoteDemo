using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewModel.Excep;
using ViewModel.Util;

namespace ViewModel
{
    public sealed class MainViewModel : ViewModelBase
    {
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

        public delegate void Notify(string menuItemName);
        public event Notify NewMenuItemEvent;

        public MainViewModel()
        {
            
        }


        private void OpenMenuItem(object param)
        {
            try
            {
                OnCursorHandling(true);

                if (param == null)
                {
                    return;
                }

                string menuItemName = param is object[]? Convert.ToString((param as object[])[0]) : Convert.ToString(param);

                if (String.IsNullOrEmpty(menuItemName))
                {
                    throw new MessageException("There is no class attached to the menu item.");
                }
                else
                {
                    NewMenuItemEvent.Invoke(menuItemName);
                }

            }
            catch (MessageException mex)
            {
                OnMessageBoxHandling(mex.Message);
            }
            catch(Exception ex)
            {
                OnMessageBoxHandling(ex.Message);
            }
            finally
            {
                OnCursorHandling(false);
            }
        }
    }
}
