using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;
using ViewModel.Excep;

namespace CustomDeliveryNoteDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Ctors

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.DataContext = new MainViewModel();

            ((MainViewModel)this.DataContext).NewMenuItemEvent += MainWindow_NewMenuItemEvent;
            ((MainViewModel)this.DataContext).NewWindowEvent += MainWindow_NewWindowEvent;
            ((MainViewModel)this.DataContext).MessageBoxEvent += MainWindow_MessageBoxEvent;
            ((MainViewModel)this.DataContext).MouseEvent += MainWindow_MouseEvent;
        }

        #endregion

        #region Events

        /// <summary>
        /// Change the mouse cursor.
        /// </summary>
        /// <param name="isWaiting"></param>
        private void MainWindow_MouseEvent(bool isWaiting)
        {
            if (isWaiting)
            {
                Mouse.OverrideCursor = Cursors.Wait;
            }
            else
            {
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Show the MessageBox.
        /// </summary>
        /// <param name="msg"></param>
        private void MainWindow_MessageBoxEvent(string msg, DeliveryNoteMessageBoxType type)
        {
            DeliveryNoteMessageBox.Show(msg, type);
        }

        /// <summary>
        /// Add new menu item based on the view's name.
        /// </summary>
        /// <param name="menuItemName"></param>
        private void MainWindow_NewMenuItemEvent(string menuItemName)
        {
            object ucType = menuItemName;
            Type t = Type.GetType((string)ucType);
            UserControl uc = Activator.CreateInstance(t) as UserControl;
            this.grdWorkPlace.Children.Add(uc);
        }

        private void MainWindow_NewWindowEvent(string windowName)
        {
            object windowType = windowName;
            Type t = Type.GetType((string)windowType);
            Window w = Activator.CreateInstance(t) as Window;
            w.Show();
        }

        /// <summary>
        /// Need to call the DragMove, because of the custom window template.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        #endregion
    }
}
