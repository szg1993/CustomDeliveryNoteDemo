using System;
using System.Collections.Generic;
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

namespace CustomDeliveryNoteDemo
{
    /// <summary>
    /// Interaction logic for RecipientMaintenanceView.xaml
    /// </summary>
    public partial class RecipientMaintenanceView : Window
    {
        #region Ctors

        public RecipientMaintenanceView()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.DataContext = new RecipientMaintenanceViewModel();

            ((RecipientMaintenanceViewModel)this.DataContext).MessageBoxEvent += RecipientMaintenanceView_MessageBoxEvent;
            ((RecipientMaintenanceViewModel)this.DataContext).MouseEvent += RecipientMaintenanceView_MouseEvent;
        }

        #endregion

        #region Events

        private void RecipientMaintenanceView_MouseEvent(bool isWaiting)
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

        private void RecipientMaintenanceView_MessageBoxEvent(string msg, DeliveryNoteMessageBoxType type)
        {
            this.Dispatcher.Invoke(() => DeliveryNoteMessageBox.Show(msg, type));
        }

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
