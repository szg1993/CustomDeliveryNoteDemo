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
        public static string MenuName
        {
            get { return "Note maintenance"; }        
        }

        public NoteMaintenanceView()
        {
            InitializeComponent();

            this.DataContext = new NoteMaintenanceViewModel();
            ((NoteMaintenanceViewModel)this.DataContext).MessageBoxEvent += NoteMaintenanceView_MessageBoxEvent;
            ((NoteMaintenanceViewModel)this.DataContext).MouseEvent += NoteMaintenanceView_MouseEvent;

            ((NoteMaintenanceViewModel)this.DataContext).GetRecipientList();
        }

        private void NoteMaintenanceView_MouseEvent(bool isWaiting)
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

        private void NoteMaintenanceView_MessageBoxEvent(string msg, DeliveryNoteMessageBoxType type)
        {
            DeliveryNoteMessageBox.Show(msg, type);
        }
    }
}
