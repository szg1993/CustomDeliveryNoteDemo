using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Note
    {
        public Note()
        {
            NoteLine = new HashSet<NoteLine>();
        }

        public int Id { get; set; }
        public string NoteNbr { get; set; }
        public string EkaerNbr { get; set; }
        public string CreatedBy { get; set; }
        public string AssignTo { get; set; }
        public string AssignToPhone { get; set; }
        public int RecId { get; set; }
        public decimal? TareWgt { get; set; }
        public string TareWgtUm { get; set; }
        public DateTime? ShipDate { get; set; }
        public decimal? PkgQty { get; set; }
        public string PkgScale { get; set; }
        public decimal? PkgSizeX { get; set; }
        public decimal? PkgSizeY { get; set; }
        public decimal? PkgSizeZ { get; set; }
        public string PkgSizeUm { get; set; }
        public string TakeoverPlace { get; set; }
        public DateTime? TakeoverDate { get; set; }
        public TimeSpan? TakeoverTime { get; set; }
        public DateTime? EstimatedArrivalDate { get; set; }
        public TimeSpan? EstimatedArrivalTime { get; set; }
        public string Category { get; set; }
        public string ContactPhone { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsCrackable { get; set; }
        public bool IsDangerous { get; set; }
        public bool IsOwnCost { get; set; }
        public int CargoType { get; set; }
        public bool IsInsuranceNeeded { get; set; }
        public decimal? InsuranceCost { get; set; }
        public string InsuranceDev { get; set; }
        public string Comments { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Status { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }

        public virtual Recipient Rec { get; set; }
        public virtual ICollection<NoteLine> NoteLine { get; set; }
    }
}
