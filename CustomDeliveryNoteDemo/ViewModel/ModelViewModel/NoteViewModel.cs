﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model;
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
using ViewModel.Factory;
using ViewModel.Util;

namespace ViewModel.ModelViewModel
{
    public enum NoteStatus
    {
        NEW,
        ACCEPTED,
        INFIRMED
    }

    public class NoteViewModel : ModelViewModelBase
    {
        #region Declaration

        private long id;
        /// <summary>
        /// The id of the note.
        /// </summary>
        public long Id
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

        private long userId;
        /// <summary>
        /// The id of the note.
        /// </summary>
        public long UserId
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
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

        private long recId;
        /// <summary>
        /// The id of the recipient.
        /// </summary>
        public long RecId
        {
            get { return recId; }
            set { recId = value; OnPropertyChanged(); }
        }

        private double? tareWgt;
        /// <summary>
        /// The weight of the tare.
        /// </summary>
        [Required]
        [Description("Tare weight")]
        public double? TareWgt
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

        private double? pkgQty;
        /// <summary>
        /// the quantity of the boxes or palettes.
        /// </summary>
        [Required]
        [Description("Package quantity")]
        public double? PkgQty
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

        private double? pkgSizeX;
        /// <summary>
        /// The x dimension of the package.
        /// </summary>
        [Required]
        [Description("Package size X dimension")]
        public double? PkgSizeX
        {
            get { return pkgSizeX; }
            set { pkgSizeX = value; OnPropertyChanged(); }
        }

        private double? pkgSizeY;
        /// <summary>
        /// The y dimension of the package.
        /// </summary>
        [Required]
        [Description("Package size Y dimension")]
        public double? PkgSizeY
        {
            get { return pkgSizeY; }
            set { pkgSizeY = value; OnPropertyChanged(); }
        }

        private double? pkgSizeZ;
        /// <summary>
        /// The z dimension of the package.
        /// </summary>
        [Required]
        [Description("Package size Z dimension")]
        public double? PkgSizeZ
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

        private long status;
        /// <summary>
        /// The status of the note.
        /// </summary>
        public long Status
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
                    addLineCommand = new RelayCommand(c => AddNoteLine());
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

            this.CategoryList = StaticListFactory.GetCategoryList();
            this.TakeoverPlaceList = StaticListFactory.GetTakeoverPlaceList();
            this.PkgScaleList = StaticListFactory.GetPkgScaleList();
            this.PkgSizeUmList = StaticListFactory.GetSizeUnitList();
            this.TareWeightUmList = StaticListFactory.GetWeightUnitList();
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return this.NoteNbr;
        }

        /// <summary>
        /// Add a new line to the note.
        /// </summary>
        private void AddNoteLine()
        {
            if (this.NoteLineVMList != null)
            {
                int lineCount = this.NoteLineVMList.Count + 1;
                this.NoteLineVMList.Add(new NoteLineViewModel(this) { Line = lineCount });
            }
        }

        #endregion
    }
}
