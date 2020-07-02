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
        public NoteMaintenanceViewModel NoteMaintenanceViewModel { get; set; }

        public static string MenuName
        {
            get { return "Note maintenance"; }        
        }

        public NoteMaintenanceView()
        {
            InitializeComponent();
            this.NoteMaintenanceViewModel = new NoteMaintenanceViewModel();
            this.DataContext = this.NoteMaintenanceViewModel;
        }
    }
}
