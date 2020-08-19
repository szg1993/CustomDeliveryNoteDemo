using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Commands;
using ViewModel.Excep;
using ViewModel.Util;

namespace ViewModel.ModelViewModel
{
    public class NoteLineViewModel: ModelViewModelBase
    {
        #region Declaration

        private long id;
        /// <summary>
        /// The ID of the line.
        /// </summary>
        public long Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private long noteId;
        /// <summary>
        /// The ID of the parent note.
        /// </summary>
        public long NoteId
        {
            get { return noteId; }
            set { noteId = value; OnPropertyChanged(); }
        }

        private long line;
        /// <summary>
        /// The number of the actual line.
        /// </summary>
        public long Line
        {
            get { return line; }
            set { line = value; OnPropertyChanged(); }
        }

        private string partCode;
        /// <summary>
        /// The item code on the note line.
        /// </summary>
        [Required]
        [Description("Line part code")]
        public string PartCode
        {
            get { return partCode; }
            set { partCode = value; OnPropertyChanged(); }
        }

        private string partDesc;
        /// <summary>
        /// The description of the part code.
        /// </summary>
        [Required]
        [Description("Line part description")]
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

        private double? partQty;
        /// <summary>
        /// The quantity of the desired part.
        /// </summary>
        [Required]
        [Description("Line part quantity")]
        public double? PartQty
        {
            get { return partQty; }
            set { partQty = value; OnPropertyChanged(); }
        }

        private string partQtyUm;
        /// <summary>
        /// The unit of the part quantity.
        /// </summary>
        [Required]
        [Description("Line part quantity unit")]
        public string PartQtyUm
        {
            get { return partQtyUm; }
            set { partQtyUm = value; OnPropertyChanged(); }
        }

        private double? partWgt;
        /// <summary>
        /// The weight of the part.
        /// </summary>
        [Required]
        [Description("Line part weight")]
        public double? PartWgt
        {
            get { return partWgt; }
            set { partWgt = value; OnPropertyChanged(); }
        }

        private string partWgtUm;
        /// <summary>
        /// The unit of the weight.
        /// </summary>
        [Required]
        [Description("Line part weight unit")]
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

        private AsyncObservableCollection<string> partQtyUmList = new AsyncObservableCollection<string>();
        /// <summary>
        /// The list of the available tare weight units.
        /// </summary>
        public AsyncObservableCollection<string> PartQtyUmList
        {
            get { return partQtyUmList; }
            set { partQtyUmList = value; OnPropertyChanged(); }
        }

        private AsyncObservableCollection<string> partWeightUmList = new AsyncObservableCollection<string>();
        /// <summary>
        /// The list of the available tare weight units.
        /// </summary>
        public AsyncObservableCollection<string> PartWeightUmList
        {
            get { return partWeightUmList; }
            set { partWeightUmList = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        private RelayCommand deleteLineCommand;

        public RelayCommand DeleteLineCommand
        {
            get
            {
                if (deleteLineCommand == null)
                {
                    deleteLineCommand = new RelayCommand(c => this.NoteVM.NoteLineVMList.Remove(this));
                }

                return deleteLineCommand;
            }
        }

        #endregion

        #region Ctors

        public NoteLineViewModel()
        {
            
        }

        public NoteLineViewModel(NoteViewModel noteVM)
        {
            this.NoteVM = noteVM;
            Task.Run(() => GetPartQtyUmListAsync());
            Task.Run(() => GetPartWeightUmListAsync());
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return this.NoteVM.NoteNbr + "/" + this.Line;
        }

        #endregion

        #region Tasks

        /// <summary>
        /// Get the list of the available part qty units async.
        /// If this weren't a demo app, then this values should be come from database or something else.
        /// </summary>
        /// <returns></returns>
        private async Task GetPartQtyUmListAsync()
        {
            await Task.Run(() =>
            {
                this.PartQtyUmList.Clear();
                this.PartQtyUmList.Add("piece");
                this.PartQtyUmList.Add("kg");
                this.PartQtyUmList.Add("cm");
                this.PartQtyUmList.Add("m");
                this.PartQtyUmList.Add("l");
            });
        }

        /// <summary>
        /// Get the list of the available part qty units async.
        /// If this weren't a demo app, then this values should be come from database or something else.
        /// </summary>
        /// <returns></returns>
        private async Task GetPartWeightUmListAsync()
        {
            await Task.Run(() =>
            {
                this.PartWeightUmList.Clear();
                this.PartWeightUmList.Add("kg");
                this.PartWeightUmList.Add("t");
            });
        }

        #endregion
    }
}
