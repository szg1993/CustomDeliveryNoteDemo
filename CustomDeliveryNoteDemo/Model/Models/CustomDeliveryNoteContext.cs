using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Model.Models
{
    public partial class CustomDeliveryNoteContext : DbContext
    {
        public CustomDeliveryNoteContext()
        {
        }

        public CustomDeliveryNoteContext(DbContextOptions<CustomDeliveryNoteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<NoteLine> NoteLine { get; set; }
        public virtual DbSet<Recipient> Recipient { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CustomDeliveryNote;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AssignTo)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.AssignToPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EkaerNbr).HasMaxLength(30);

                entity.Property(e => e.EstimatedArrivalDate).HasColumnType("date");

                entity.Property(e => e.ModifiedDate)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.NoteNbr)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.PkgQty).HasColumnType("decimal(8, 4)");

                entity.Property(e => e.PkgScale)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PkgSizeUm)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PkgSizeX).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.PkgSizeY).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.PkgSizeZ).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.RecId).HasColumnName("RecID");

                entity.Property(e => e.ShipDate).HasColumnType("date");

                entity.Property(e => e.TakeoverDate).HasColumnType("date");

                entity.Property(e => e.TakeoverPlace)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.TareWgt).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.TareWgtUm)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Rec)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.RecId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Note_Recipient");
            });

            modelBuilder.Entity<NoteLine>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.NoteId).HasColumnName("NoteID");

                entity.Property(e => e.PartCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PartDesc).IsRequired();

                entity.Property(e => e.PartQty).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.PartQtyUm)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PartWgt).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.PartWgtUm)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.NoteLine)
                    .HasForeignKey(d => d.NoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NoteLine_Note");
            });

            modelBuilder.Entity<Recipient>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Zip)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
