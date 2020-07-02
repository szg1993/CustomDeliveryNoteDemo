//using CustomDeliveryNoteDemo.Util;
using System;
using System.Collections.Generic;
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


        public MainViewModel()
        {
            
        }


        private void OpenMenuItem(object param)
        {
            if (param == null)
            {
                return;
            }

            string className = param is object[]? Convert.ToString((param as object[])[0]) : Convert.ToString(param);

            if (String.IsNullOrEmpty(className))
            {
                throw new MessageException("There is no class attached to the menu item.");
            }

            
        }
    }
}
