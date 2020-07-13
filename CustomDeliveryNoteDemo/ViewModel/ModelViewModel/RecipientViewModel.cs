using AutoMapper;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ViewModel.Excep;

namespace ViewModel.ModelViewModel
{
    public class RecipientViewModel: ModelViewModelBase
    {
        #region Properties

        private int id;
        /// <summary>
        /// The ID of the recipient.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string code;
        /// <summary>
        /// The code of the recipient.
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged(); }
        }

        private string name;
        /// <summary>
        /// The name of the recipient.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string country;
        /// <summary>
        /// The country of the recipient.
        /// </summary>
        public string Country
        {
            get { return country; }
            set { country = value; OnPropertyChanged(); }
        }

        private string zip;
        /// <summary>
        /// The Zip code the recipient.
        /// </summary>
        public string Zip
        {
            get { return zip; }
            set { zip = value; OnPropertyChanged(); }
        }

        private string city;
        /// <summary>
        /// The city of the recipient.
        /// </summary>
        public string City
        {
            get { return city; }
            set { city = value; OnPropertyChanged(); }
        }

        private string address;
        /// <summary>
        /// The address of the recipient.
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }

        private bool isOutlander;
        /// <summary>
        /// True when the recipient is out of the borders.
        /// </summary>
        public bool IsOutlander
        {
            get { return isOutlander; }
            set { isOutlander = value; OnPropertyChanged(); }
        }

        private ICollection<NoteViewModel> noteVMList;
        /// <summary>
        /// The note view models of this recipient.
        /// </summary>
        public ICollection<NoteViewModel> NoteVMList
        {
            get { return noteVMList; }
            set { noteVMList = value; OnPropertyChanged(); }
        }

        #endregion

        #region Ctors

        public RecipientViewModel()
        {
            this.NoteVMList = new HashSet<NoteViewModel>();
        }

        #endregion

        /// <summary>
        /// Validate the properties of the recipient.
        /// </summary>
        public override void CheckErrors()
        {
            if (String.IsNullOrEmpty(this.Name))
            {
                throw new MessageException("The name of the recipient cannot be empty.");
            }
            else if (String.IsNullOrEmpty(this.Code))
            {
                throw new MessageException("The code of the recipient cannot be empty.");
            }
            else if (String.IsNullOrEmpty(this.Country))
            {
                throw new MessageException("The country of the recipient cannot be empty.");
            }
            else if (String.IsNullOrEmpty(this.Zip))
            {
                throw new MessageException("The ZIP code of the recipient cannot be empty.");
            }
            else if (String.IsNullOrEmpty(this.City))
            {
                throw new MessageException("The city of the recipient cannot be empty.");
            }
            else if (String.IsNullOrEmpty(this.Address))
            {
                throw new MessageException("The city of the recipient cannot be empty.");
            }
        }
    }
}
