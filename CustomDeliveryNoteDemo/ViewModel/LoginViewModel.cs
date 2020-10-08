using AutoMapper;
using Model;
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

        #region Ctors

        public LoginViewModel()
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Do the login logic.
        /// </summary>
        /// <param name="parameter"></param>
        private void Login(object parameter)
        {
            try
            {
                OnCursorHandling(true);

                if (parameter is IHavePassword passwordContainer)
                {
                    SecureString secureString = passwordContainer.Password;
                    string password = SecurityUtilities.ConvertToUnsecureString(secureString);
                    
                    var a = CheckCreadentials(password);
                    
                    OpenMenuItem("CustomDeliveryNoteDemo.MainWindow");
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
        private UserViewModel CheckCreadentials(string password)
        {
            using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
            {
                User user = ctx.User.FirstOrDefault(x => x.Name == this.UserName && x.Password == password);

                if (user != null)
                {
                    Mapper mapper = new Mapper(MapperConfig);
                    return mapper.Map<UserViewModel>(user);
                }

                throw new MessageException("The entered credentials are invalid.");
            }
        }

        #endregion
    }
}
