using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Commands;
using ViewModel.Excep;
using ViewModel.Util;

namespace ViewModel.ModelViewModel
{
    public class NoteViewModel : ModelViewModelBase
    {
        #region Properties

        private int id;
        /// <summary>
        /// The id of the note.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string noteNbr;
        /// <summary>
        /// The number of the note.
        /// </summary>
        [Required]
        [Description("Note number")]
        public string NoteNbr
        {
            get { return noteNbr;}
            set { noteNbr = value; OnPropertyChanged(); }
        }

        private string ekaerNbr;
        /// <summary>
        /// The ekaer number of the note, if needed.
        /// </summary>
        public string EkaerNbr
        {
            get { return ekaerNbr; }
            set { ekaerNbr = value; OnPropertyChanged(); }
        }

        private string createdBy;
        /// <summary>
        /// The creator of the note.
        /// </summary>
        [Required]
        [Description("Date of creation")]
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; OnPropertyChanged(); }
        }

        private string assignTo;
        /// <summary>
        /// The person who responsible for the note from sender side.
        /// </summary>
        [Required]
        [Description("Assigned to")]
        public string AssignTo
        {
            get { return assignTo; }
            set { assignTo = value; OnPropertyChanged(); }
        }

        private string assignToPhone;
        /// <summary>
        /// The phone number of the responsible person.
        /// </summary>
        [Required]
        [Description("Assigned to phone")]
        public string AssignToPhone
        {
            get { return assignToPhone; }
            set { assignToPhone = value; OnPropertyChanged(); }
        }

        private int recId;
        /// <summary>
        /// The id of the recipient.
        /// </summary>
        public int RecId
        {
            get { return recId; }
            set { recId = value; OnPropertyChanged(); }
        }

        private decimal tareWgt;
        /// <summary>
        /// The weight of the tare.
        /// </summary>
        [Required]
        [Description("Tare weight")]
        public decimal TareWgt
        {
            get { return tareWgt; }
            set { tareWgt = value; OnPropertyChanged(); }
        }

        private string tareWgtUm;
        /// <summary>
        /// The unit of the tare weight.
        /// </summary>
        [Required]
        [Description("Tare weight unit")]
        public string TareWgtUm
        {
            get { return tareWgtUm; }
            set { tareWgtUm = value; OnPropertyChanged(); }
        }

        private DateTime? shipDate;
        /// <summary>
        /// The ship date of the delivery note.
        /// </summary>
        [Required]
        [Description("Ship date")]
        public DateTime? ShipDate
        {
            get { return shipDate; }
            set { shipDate = value; OnPropertyChanged(); }
        }

        private decimal pkgQty;
        /// <summary>
        /// the quantity of the boxes or palettes.
        /// </summary>
        [Required]
        [Description("Package quantity")]
        public decimal PkgQty
        {
            get { return pkgQty; }
            set { pkgQty = value; OnPropertyChanged(); }
        }

        private string pkgScale;
        /// <summary>
        /// The scale of the package
        /// </summary>
        [Required]
        [Description("Package scale")]
        public string PkgScale
        {
            get { return pkgScale; }
            set { pkgScale = value; OnPropertyChanged(); }
        }

        private decimal pkgSizeX;
        /// <summary>
        /// The x dimension of the package.
        /// </summary>
        [Required]
        [Description("Package size X dimension")]
        public decimal PkgSizeX
        {
            get { return pkgSizeX; }
            set { pkgSizeX = value; OnPropertyChanged(); }
        }

        private decimal pkgSizeY;
        /// <summary>
        /// The y dimension of the package.
        /// </summary>
        [Required]
        [Description("Package size Y dimension")]
        public decimal PkgSizeY
        {
            get { return pkgSizeY; }
            set { pkgSizeY = value; OnPropertyChanged(); }
        }

        private decimal pkgSizeZ;
        /// <summary>
        /// The z dimension of the package.
        /// </summary>
        [Required]
        [Description("Package size Z dimension")]
        public decimal PkgSizeZ
        {
            get { return pkgSizeZ; }
            set { pkgSizeZ = value; OnPropertyChanged(); }
        }

        private string pkgSizeUm;
        /// <summary>
        /// The unit of the package size.
        /// </summary>
        [Required]
        [Description("Package size unit")]
        public string PkgSizeUm
        {
            get { return pkgSizeUm; }
            set { pkgSizeUm = value; OnPropertyChanged(); }
        }

        private string takeoverPlace;
        /// <summary>
        /// The place where sender will pick up the package.
        /// </summary>
        [Required]
        [Description("Place of receipt")]
        public string TakeoverPlace
        {
            get { return takeoverPlace; }
            set { takeoverPlace = value; OnPropertyChanged(); }
        }

        private DateTime? takeoverDate;
        /// <summary>
        /// The date of the take over.
        /// </summary>
        [Required]
        [Description("Date of receipt")]
        public DateTime? TakeoverDate
        {
            get { return takeoverDate; }
            set { takeoverDate = value; OnPropertyChanged(); }
        }

        private DateTime? estimatedArrivalDate;
        /// <summary>
        /// The estimated date when the package arrives to the destination.
        /// </summary>
        [Required]
        [Description("Estimated arrival date")]
        public DateTime? EstimatedArrivalDate
        {
            get { return estimatedArrivalDate; }
            set { estimatedArrivalDate = value; OnPropertyChanged(); }
        }

        private string category;
        /// <summary>
        /// The category of the Note.
        /// </summary>
        [Required]
        [Description("Note category")]
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged(); }
        }

        private string contact;
        /// <summary>
        /// The contact at the target company.
        /// </summary>
        [Required]
        [Description("Contact (sender)")]
        public string Contact
        {
            get { return contact; }
            set { contact = value; OnPropertyChanged(); }
        }

        private string contactPhone;
        /// <summary>
        /// The contact phone at the target company.
        /// </summary>
        [Required]
        [Description("Contact phone (sender)")]
        public string ContactPhone
        {
            get { return contactPhone; }
            set { contactPhone = value; OnPropertyChanged(); }
        }

        private bool isUrgent;
        /// <summary>
        /// Is the package urgent or not?
        /// </summary>
        public bool IsUrgent
        {
            get { return isUrgent; }
            set { isUrgent = value; OnPropertyChanged(); }
        }

        private bool isCrackable;
        /// <summary>
        /// Is the package crackable or not?
        /// </summary>
        public bool IsCrackable
        {
            get { return isCrackable; }
            set { isCrackable = value; OnPropertyChanged(); }
        }

        private bool isDangerous;
        /// <summary>
        /// Is the package dangerous or not?
        /// </summary>
        public bool IsDangerous
        {
            get { return isDangerous; }
            set { isDangerous = value; OnPropertyChanged(); }
        }

        private bool isOwnCost;
        /// <summary>
        /// Is the cost of the delivery belongs to the sender?
        /// </summary>
        public bool IsOwnCost
        {
            get { return isOwnCost; }
            set { isOwnCost = value; OnPropertyChanged(); }
        }

        private string comments;
        /// <summary>
        /// Comments.
        /// </summary>
        public string Comments
        {
            get { return comments; }
            set { comments = value; OnPropertyChanged(); }
        }

        private DateTime createdDate;
        /// <summary>
        /// The creation date of the note.
        /// </summary>
        [Required]
        [Description("Date of creation")]
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; OnPropertyChanged(); }
        }

        private int status;
        /// <summary>
        /// The status of the note.
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        private RecipientViewModel recVM;
        /// <summary>
        /// The recipient of the note.
        /// </summary>
        public RecipientViewModel RecVM
        {
            get { return recVM; }
            set { recVM = value; OnPropertyChanged(); }
        }

        private ObservableCollection<NoteLineViewModel> noteLineVMList;
        /// <summary>
        /// The line view models of the note.
        /// </summary>
        public ObservableCollection<NoteLineViewModel> NoteLineVMList
        {
            get { return noteLineVMList; }
            set { noteLineVMList = value; OnPropertyChanged(); }
        }

        private AsyncObservableCollection<string> categoryList = new AsyncObservableCollection<string>();
        /// <summary>
        /// The list of the available delivery note categories.
        /// </summary>
        public AsyncObservableCollection<string> CategoryList
        {
            get { return categoryList; }
            set { categoryList = value; OnPropertyChanged(); }
        }

        private AsyncObservableCollection<string> takeoverPlaceList = new AsyncObservableCollection<string>();
        /// <summary>
        /// The list of the available takeover places.
        /// </summary>
        public AsyncObservableCollection<string> TakeoverPlaceList
        {
            get { return takeoverPlaceList; }
            set { takeoverPlaceList = value; OnPropertyChanged(); }
        }

        private AsyncObservableCollection<string> pkgScaleList = new AsyncObservableCollection<string>();
        /// <summary>
        /// The list of the available package scales.
        /// </summary>
        public AsyncObservableCollection<string> PkgScaleList
        {
            get { return pkgScaleList; }
            set { pkgScaleList = value; OnPropertyChanged(); }
        }

        private AsyncObservableCollection<string> pkgSizeUmList = new AsyncObservableCollection<string>();
        /// <summary>
        /// The list of the available package size units.
        /// </summary>
        public AsyncObservableCollection<string> PkgSizeUmList
        {
            get { return pkgSizeUmList; }
            set { pkgSizeUmList = value; OnPropertyChanged(); }
        }

        private AsyncObservableCollection<string> tareWeightUmList = new AsyncObservableCollection<string>();
        /// <summary>
        /// The list of the available tare weight units.
        /// </summary>
        public AsyncObservableCollection<string> TareWeightUmList
        {
            get { return tareWeightUmList; }
            set { tareWeightUmList = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        private RelayCommand addLineCommand;

        public RelayCommand AddLineCommand
        {
            get
            {
                if (addLineCommand == null)
                {
                    addLineCommand = new RelayCommand(c => this.NoteLineVMList?.Add(new NoteLineViewModel(this)));
                }

                return addLineCommand;
            }
        }

        private RelayCommand deleteLinesCommand;

        public RelayCommand DeleteLinesCommand
        {
            get
            {
                if (deleteLinesCommand == null)
                {
                    deleteLinesCommand = new RelayCommand(c => this.NoteLineVMList?.Clear());
                }

                return deleteLinesCommand;
            }
        }

        #endregion

        #region Ctors

        public NoteViewModel()
        {
            this.NoteLineVMList = new ObservableCollection<NoteLineViewModel>();
            this.RecVM = new RecipientViewModel();
            this.CreatedBy = "admin"; //Hard coded because this is just a demo application without login system.

            Task.Run(() => GetCategoryListAsync());
            Task.Run(() => GetTakeoverPlaceListAsync());
            Task.Run(() => GetPkgScaleListAsync());
            Task.Run(() => GetPkgSizeUmListAsync());
            Task.Run(() => GetTareWeightUmListAsync());
        }

        #endregion

        #region Methods

        #endregion

        #region Static

        /// <summary>
        /// Create a new unique ID for the deliver note.
        /// </summary>
        /// <returns></returns>
        public static string CreateNoteNbr(CustomDeliveryNoteContext ctx)
        {
            string prefix = "N";
            string postfix;
            string nbrSerialFormat = "00000";

            Note lastNote = ctx.Note.OrderByDescending(x => x.Id).FirstOrDefault();

            if (lastNote != null)
            {
                postfix = String.IsNullOrEmpty(lastNote.NoteNbr) ? nbrSerialFormat : lastNote.NoteNbr.Substring(prefix.Length, nbrSerialFormat.Length);

                if (Convert.ToInt32(postfix) < Convert.ToInt32(nbrSerialFormat.Replace('0', '5')))
                {
                    return prefix + (Convert.ToInt32(postfix) + 1).ToString(nbrSerialFormat);
                }
                else
                {
                    throw new MessageException("The program is unable to calculate the next delivery note number, because there is more available serial number.");
                }
            }
            else
            {
                return prefix + "00001";
            }
        }

        #endregion

        #region Tasks

        /// <summary>
        /// Get the list of the available categories async.
        /// </summary>
        /// <returns></returns>
        private async Task GetCategoryListAsync()
        {
            await Task.Run(() =>
            {
                this.CategoryList.Clear();
                this.CategoryList.Add("Other");
                this.CategoryList.Add("Quality complaint");
                this.CategoryList.Add("Return cargo");
                this.CategoryList.Add("Sample");
                this.CategoryList.Add("Tool test");
            });
        }

        /// <summary>
        /// Get the list of the available takeover places async.
        /// </summary>
        /// <returns></returns>
        private async Task GetTakeoverPlaceListAsync()
        {
            await Task.Run(() =>
            {
                this.TakeoverPlaceList.Clear();
                this.TakeoverPlaceList.Add("Logistics office");
                this.TakeoverPlaceList.Add("Special storage");
                this.TakeoverPlaceList.Add("Tooling storage");
            });
        }

        /// <summary>
        /// Get the list of the available package scales async.
        /// </summary>
        /// <returns></returns>
        private async Task GetPkgScaleListAsync()
        {
            await Task.Run(() =>
            {
                this.PkgScaleList.Clear();
                this.PkgScaleList.Add("Box");
                this.PkgScaleList.Add("Envelope");
                this.PkgScaleList.Add("Pallet");
            });
        }

        /// <summary>
        /// Get the list of the available package size units async.
        /// </summary>
        /// <returns></returns>
        private async Task GetPkgSizeUmListAsync()
        {
            await Task.Run(() =>
            {
                this.PkgSizeUmList.Clear();
                this.PkgSizeUmList.Add("cm");
                this.PkgSizeUmList.Add("mm");
            });
        }

        /// <summary>
        /// Get the list of the available tare weight units async.
        /// </summary>
        /// <returns></returns>
        private async Task GetTareWeightUmListAsync()
        {
            await Task.Run(() =>
            {
                this.TareWeightUmList.Clear();
                this.TareWeightUmList.Add("kg");
                this.TareWeightUmList.Add("t");
            });
        }

        #endregion
    }
}
