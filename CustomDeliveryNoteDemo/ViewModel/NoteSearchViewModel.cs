using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModel.ModelViewModel;

namespace ViewModel
{
    public class NoteSearchViewModel : ViewModelBase
    {
        #region Declaration

        private ObservableCollection<NoteLineViewModel> noteLineVMList = new ObservableCollection<NoteLineViewModel>();

        public ObservableCollection<NoteLineViewModel> NoteLineVMList
        {
            get { return noteLineVMList; }
            set { noteLineVMList = value; OnPropertyChanged(); }
        }

        #endregion

        #region Ctors

        public NoteSearchViewModel()
        {

        }

        #endregion
    }
}
