using System;
using System.Collections.Generic;

namespace Model
{
    public partial class NoteLine
    {
        public long Id { get; set; }
        public long NoteId { get; set; }
        public long Line { get; set; }
        public string PartCode { get; set; }
        public string PartDesc { get; set; }
        public string PartCmt { get; set; }
        public double PartQty { get; set; }
        public string PartQtyUm { get; set; }
        public double PartWgt { get; set; }
        public string PartWgtUm { get; set; }

        public virtual Note Note { get; set; }
    }
}
