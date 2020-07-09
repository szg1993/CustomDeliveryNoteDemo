using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Commands;

namespace ViewModel.ModelViewModel
{
    public class NoteLineViewModel: ModelViewModelBase
    {
        #region Declaration

        private int id;
        /// <summary>
        /// The ID of the line.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private int noteId;
        /// <summary>
        /// The ID of the parent note.
        /// </summary>
        public int NoteId
        {
            get { return noteId; }
            set { noteId = value; OnPropertyChanged(); }
        }

        private int line;
        /// <summary>
        /// The number of the actual line.
        /// </summary>
        public int Line
        {
            get { return line; }
            set { line = value; OnPropertyChanged(); }
        }

        private string partCode;
        /// <summary>
        /// The item code on the note line.
        /// </summary>
        public string PartCode
        {
            get { return partCode; }
            set { partCode = value; OnPropertyChanged(); }
        }

        private string partDesc;
        /// <summary>
        /// The description of the part code.
        /// </summary>
        public string PartDesc
        {
            get { return partDesc; }
            set { partDesc = value; OnPropertyChanged(); }
        }

        private string partCmt;
        /// <summary>
        /// Comment for the line.
        /// </summary>
        public string PartCmt
        {
            get { return partCmt; }
            set { partCmt = value; OnPropertyChanged(); }
        }

        private decimal? partQty;
        /// <summary>
        /// The quantity of the desired part.
        /// </summary>
        public decimal? PartQty
        {
            get { return partQty; }
            set { partQty = value; OnPropertyChanged(); }
        }

        private string partUm;
        /// <summary>
        /// The unit of the part quantity.
        /// </summary>
        public string PartUm
        {
            get { return partUm; }
            set { partUm = value; OnPropertyChanged(); }
        }

        private decimal? partWgt;
        /// <summary>
        /// The weight of the part.
        /// </summary>
        public decimal? PartWgt
        {
            get { return partWgt; }
            set { partWgt = value; OnPropertyChanged(); }
        }

        private string partWgtUm;
        /// <summary>
        /// The unit of the weight.
        /// </summary>
        public string PartWgtUm
        {
            get { return partWgtUm; }
            set { partWgtUm = value; OnPropertyChanged(); }
        }

        private NoteViewModel noteVM;
        /// <summary>
        /// The parent note view model of the lines.
        /// </summary>
        public NoteViewModel NoteVM
        {
            get { return noteVM; }
            set { noteVM = value; OnPropertyChanged(); }
        }

        #endregion

        #region Ctors

        public NoteLineViewModel(NoteViewModel noteVM)
        {
            this.NoteVM = noteVM;
        }

        #endregion
    }
}
