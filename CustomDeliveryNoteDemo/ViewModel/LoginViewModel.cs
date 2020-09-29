using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Commands;

namespace ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string userName;
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        private string password;
        /// <summary>
        /// The password of the user.
        /// Sorry for the security issue with the string password. :)
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        #region Commands

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                if (this.loginCommand == null)
                {
                    this.loginCommand = new RelayCommand(x => Login());
                }

                return loginCommand;
            }
        }

        #endregion

        #region Ctors

        public LoginViewModel()
        {

        }

        #endregion

        #region Methods

        private void Login()
        {
            OpenMenuItem("CustomDeliveryNoteDemo.MainWindow");
        }

        #endregion
    }
}
