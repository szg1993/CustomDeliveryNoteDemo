using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using ViewModel.ModelViewModel;

namespace ViewModel
{
    public class NoteMaintenanceViewModel : ViewModelBase
    {
        #region Declaration

        private NoteViewModel actNoteVM;
        /// <summary>
        /// The actual note.
        /// </summary>
        public NoteViewModel ActNoteVM
        {
            get { return actNoteVM; }
            set { actNoteVM = value; OnPropertyChanged(); }
        }

        private ObservableCollection<RecipientViewModel> allRecipientVMList;
        /// <summary>
        /// The list of the selectable recipients.
        /// </summary>
        public ObservableCollection<RecipientViewModel> AllRecipientVMList
        {
            get { return allRecipientVMList; }
            set { allRecipientVMList = value; OnPropertyChanged(); }
        }

        #endregion

        #region Ctors

        public NoteMaintenanceViewModel()
        {
            this.ActNoteVM = new NoteViewModel();
            //this.AllRecipientVMList = RecipientViewModel.GetAllRecipient();
        }

        #endregion
    }
}
