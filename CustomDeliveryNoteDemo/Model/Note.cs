using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Note
    {
        public Note()
        {
            NoteLine = new HashSet<NoteLine>();
        }

        public long Id { get; set; }
        public string NoteNbr { get; set; }
        public string EkaerNbr { get; set; }
        public string CreatedBy { get; set; }
        public string AssignTo { get; set; }
        public string AssignToPhone { get; set; }
        public long RecId { get; set; }
        public double TareWgt { get; set; }
        public string TareWgtUm { get; set; }
        public long ShipDate { get; set; }
        public double PkgQty { get; set; }
        public string PkgScale { get; set; }
        public double PkgSizeX { get; set; }
        public double PkgSizeY { get; set; }
        public double PkgSizeZ { get; set; }
        public string PkgSizeUm { get; set; }
        public string TakeoverPlace { get; set; }
        public long TakeoverDate { get; set; }
        public string EstimatedArrivalDate { get; set; }
        public string Category { get; set; }
        public string Contact { get; set; }
        public string ContactPhone { get; set; }
        public long IsUrgent { get; set; }
        public long IsCrackable { get; set; }
        public long IsDangerous { get; set; }
        public long IsOwnCost { get; set; }
        public string Comments { get; set; }
        public long CreatedDate { get; set; }
        public long Status { get; set; }

        public virtual Recipient Rec { get; set; }
        public virtual ICollection<NoteLine> NoteLine { get; set; }
    }
}
