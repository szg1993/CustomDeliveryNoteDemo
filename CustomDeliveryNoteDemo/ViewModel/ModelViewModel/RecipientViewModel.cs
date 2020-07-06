using Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ViewModel.ModelViewModel
{
    public class RecipientViewModel: ModelViewModelBase
    {
        #region Declaration

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

        #region Static methods

        //public static ObservableCollection<RecipientViewModel> GetAllRecipient()
        //{
        //    List<Recipient> allRecipientList = new List<Recipient>();
                
        //    using (CustomDeliveryNoteContext ctx = new CustomDeliveryNoteContext())
        //    {
        //        allRecipientList = ctx.Recipient.Where(x => x.Code != null).ToList();
        //    }
        //}

        #endregion
    }
}
