using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Model
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
                optionsBuilder.UseSqlite("Filename=CustomDeliveryNote.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AssignTo).IsRequired();

                entity.Property(e => e.AssignToPhone).IsRequired();

                entity.Property(e => e.Category).IsRequired();

                entity.Property(e => e.Contact).IsRequired();

                entity.Property(e => e.ContactPhone).IsRequired();

                entity.Property(e => e.CreatedBy).IsRequired();

                entity.Property(e => e.EstimatedArrivalDate).IsRequired();

                entity.Property(e => e.NoteNbr).IsRequired();

                entity.Property(e => e.PkgScale).IsRequired();

                entity.Property(e => e.PkgSizeUm).IsRequired();

                entity.Property(e => e.RecId).HasColumnName("RecID");

                entity.Property(e => e.TakeoverPlace).IsRequired();

                entity.Property(e => e.TareWgtUm).IsRequired();

                entity.HasOne(d => d.Rec)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.RecId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<NoteLine>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NoteId).HasColumnName("NoteID");

                entity.Property(e => e.PartCode).IsRequired();

                entity.Property(e => e.PartWgtUm).IsRequired();

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.NoteLine)
                    .HasForeignKey(d => d.NoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Recipient>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Country).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Zip).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
