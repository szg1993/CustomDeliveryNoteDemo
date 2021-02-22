using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class User
    {
        public User()
        {
            Note = new HashSet<Note>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Note> Note { get; set; }
    }
}
