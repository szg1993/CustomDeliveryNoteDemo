using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CustomDeliveryNoteDemo
{
    /// <summary>
    /// Interaction logic for NoteMaintenanceView.xaml
    /// </summary>
    public partial class NoteMaintenanceView : UserControl
    {
        #region Ctors

        public NoteMaintenanceView()
        {
            InitializeComponent();

            this.DataContext = new NoteMaintenanceViewModel();
            ((NoteMaintenanceViewModel)this.DataContext).MessageBoxEvent += NoteMaintenanceView_MessageBoxEvent;
            ((NoteMaintenanceViewModel)this.DataContext).MouseEvent += NoteMaintenanceView_MouseEvent;
            ((NoteMaintenanceViewModel)this.DataContext).ComboBoxEvent += NoteMaintenanceView_ComboBoxEvent;

            ((NoteMaintenanceViewModel)this.DataContext).CallGetRecipientList();
        }

        #endregion

        #region Events

        private void NoteMaintenanceView_ComboBoxEvent()
        {
            this.Dispatcher.Invoke(() => this.cmbRecipient.SelectedItem = null);
        }

        private void NoteMaintenanceView_MouseEvent(bool isWaiting)
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

        private void NoteMaintenanceView_MessageBoxEvent(string msg, DeliveryNoteMessageBoxType type)
        {
            this.Dispatcher.Invoke(() => DeliveryNoteMessageBox.Show(msg, type));
        }

        #endregion
    }
}
