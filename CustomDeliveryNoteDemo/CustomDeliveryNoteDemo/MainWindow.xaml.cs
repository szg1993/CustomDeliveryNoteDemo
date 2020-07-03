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
            this.DataContext = new MainViewModel();

            ((MainViewModel)this.DataContext).NewMenuItemEvent += MainWindow_NewMenuItemEvent;
            ((MainViewModel)this.DataContext).MessageBoxEvent += MainWindow_MessageBoxEvent;
            ((MainViewModel)this.DataContext).MouseEvent += MainWindow_MouseEvent;
        }

        #endregion

        #region Events

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

        private void MainWindow_MessageBoxEvent(string msg)
        {
            MessageBox.Show(msg);
        }

        private void MainWindow_NewMenuItemEvent(string menuItemName)
        {
            object ucType = menuItemName;
            Type t = Type.GetType((string)ucType);
            Thread.Sleep(5000);
            UserControl uc = Activator.CreateInstance(t) as UserControl;
            this.grdWorkPlace.Children.Add(uc);
        }

        #endregion
    }
}
