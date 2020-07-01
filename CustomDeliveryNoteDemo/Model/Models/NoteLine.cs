using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class NoteLine
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int Line { get; set; }
        public string PartCode { get; set; }
        public string PartDesc { get; set; }
        public string PartCmt { get; set; }
        public decimal? PartQty { get; set; }
        public string PartUm { get; set; }
        public decimal? PartWgt { get; set; }
        public string PartWgtUm { get; set; }

        public virtual Note Note { get; set; }
    }
}
