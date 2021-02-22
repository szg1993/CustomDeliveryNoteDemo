using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Recipient
    {
        public Recipient()
        {
            Note = new HashSet<Note>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public long IsOutlander { get; set; }

        public virtual ICollection<Note> Note { get; set; }
    }
}
