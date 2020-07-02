//using CustomDeliveryNoteDemo.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public sealed class MainViewModel : ViewModelBase
    {
        //private RelayCommand openMenuItemCommand;

        //public RelayCommand OpenMenuItemCommand
        //{
        //    get
        //    {
        //        if (this.openMenuItemCommand == null)
        //        {
        //            this.openMenuItemCommand = new RelayCommand(OpenMenuItem);
        //        }
        //        return openMenuItemCommand;
        //    }
        //}

        public MainViewModel()
        {

        }

        private void OpenMenuItem(object param)
        {
            if (param == null)
            {
                return;
            }
        }
    }
}
