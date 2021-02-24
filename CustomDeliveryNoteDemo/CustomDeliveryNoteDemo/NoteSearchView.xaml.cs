using System;
using System.Collections.Generic;
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
    /// Interaction logic for NoteSearchView.xaml
    /// </summary>
    public partial class NoteSearchView : UserControl
    {
        #region Ctors

        public NoteSearchView()
        {
            InitializeComponent();

            this.DataContext = new NoteSearchViewModel();

            ((NoteSearchViewModel)this.DataContext).MessageBoxEvent += NoteSearchView_MessageBoxEvent;
            ((NoteSearchViewModel)this.DataContext).MouseEvent += NoteSearchView_MouseEvent;

            ((NoteSearchViewModel)this.DataContext).CallGetNoteLines();
        }

        #endregion

        #region Events

        private void NoteSearchView_MessageBoxEvent(string msg, DeliveryNoteMessageBoxType type)
        {
            this.Dispatcher.Invoke(() => DeliveryNoteMessageBox.Show(msg, type));
        }

        private void NoteSearchView_MouseEvent(bool isWaiting)
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

        #endregion
    }
}
