using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;
using ViewModel.Interfaces;

namespace CustomDeliveryNoteDemo
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window, IHavePassword
    {
        #region Declaration

        public SecureString Password
        {
            get
            {
                return pbPassword.SecurePassword;
            }
        }

        #endregion

        #region Ctors

        public LoginView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.DataContext = new LoginViewModel();

            ((LoginViewModel)this.DataContext).MessageBoxEvent += LoginView_MessageBoxEvent;
            ((LoginViewModel)this.DataContext).MouseEvent += LoginView_MouseEvent;
            ((LoginViewModel)this.DataContext).NewMenuItemEvent += LoginView_NewMenuItemEvent;
        }

        #endregion

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void LoginView_MouseEvent(bool isWaiting)
        {
            if (isWaiting)
            {
                this.Dispatcher.Invoke(() => Mouse.OverrideCursor = Cursors.Wait);
            }
            else
            {
                this.Dispatcher.Invoke(() => Mouse.OverrideCursor = null);
            }
        }

        private void LoginView_MessageBoxEvent(string msg, DeliveryNoteMessageBoxType type)
        {
            this.Dispatcher.Invoke(() => DeliveryNoteMessageBox.Show(msg, type));
        }

        private void LoginView_NewMenuItemEvent(string menuItemName)
        {
            object ucType = menuItemName;
            Type t = Type.GetType((string)ucType);

            Window w = Activator.CreateInstance(t) as Window;
            this.Close();
            w.Show();
        }
    }
}
