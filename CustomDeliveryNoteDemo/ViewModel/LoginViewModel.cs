using AutoMapper;
using Model;
using Model.Models;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Commands;
using ViewModel.Excep;
using ViewModel.Interfaces;
using ViewModel.ModelViewModel;
using ViewModel.Singleton;
using ViewModel.Util;

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

        #region Commands

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                if (this.loginCommand == null)
                {
                    this.loginCommand = new RelayCommand(x => Login(x));
                }

                return loginCommand;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Do the login logic.
        /// </summary>
        /// <param name="parameter"></param>
        private async Task Login(object parameter)
        {
            try
            {
                OnCursorHandling(true);

                if (parameter is IHavePassword passwordContainer)
                {
                    SecureString secureString = passwordContainer.Password;
                    string password = SecurityUtilities.ConvertToUnsecureString(secureString);

                    await CheckCredentials(password);

                    OpenMenuItem(StaticDetails.MainWindowName);
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

        /// <summary>
        /// Get the user view model from the database.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task CheckCredentials(string password)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new CustomDeliveryNoteContext()))
            {
                var user = await unitOfWork.UserRepo.GetUserByNameAndPasswordAsync(this.UserName, password);

                if (user == null)
                {
                    throw new MessageException("The entered credentials are invalid.");
                }

                LoggedUser.Employee.Id = user.Id;
                LoggedUser.Employee.Name = user.Name;
            }          
        }

        #endregion
    }
}
