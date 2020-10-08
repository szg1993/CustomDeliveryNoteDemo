using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.ModelViewModel
{
    public class UserViewModel : ModelViewModelBase
    {
        private int id;
        /// <summary>
        /// The ID of the user.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string name;
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private ICollection<Note> note;
        /// <summary>
        /// The note view models of this user.
        /// </summary>
        public ICollection<Note> Note
        {
            get { return note; }
            set { note = value; OnPropertyChanged(); }
        }

        public UserViewModel()
        {
            Note = new HashSet<Note>();
        }
    }
}
