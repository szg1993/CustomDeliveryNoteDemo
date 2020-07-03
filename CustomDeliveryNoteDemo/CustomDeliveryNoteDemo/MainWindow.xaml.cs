using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            try
            {
                InitializeComponent();
                this.DataContext = new MainViewModel();

                ((MainViewModel)this.DataContext).NewMenuItemEvent += MainWindow_NewMenuItemEvent;
            }
            catch (MessageException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainWindow_NewMenuItemEvent(string menuItemName)
        {
            object ucType = menuItemName;
            Type t = Type.GetType((string)ucType);
            UserControl uc = Activator.CreateInstance(t) as UserControl;
            this.grdWorkPlace.Children.Add(uc);
        }

        #endregion
    }
}
