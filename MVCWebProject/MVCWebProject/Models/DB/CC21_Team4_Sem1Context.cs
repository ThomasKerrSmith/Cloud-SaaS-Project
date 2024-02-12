using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MVCWebProject.Models.DB
{
    public partial class CC21_Team4_Sem1Context : DbContext
    {
        public CC21_Team4_Sem1Context()
        {
        }

        public CC21_Team4_Sem1Context(DbContextOptions<CC21_Team4_Sem1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<StaffDetail> StaffDetails { get; set; }
        public virtual DbSet<StudentDetail> StudentDetails { get; set; }
        public virtual DbSet<StudentGrade> StudentGrades { get; set; }
        public virtual DbSet<ItemInventory> ItemInventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=citizen.manukautech.info,6305; Database = CC21_Team4_Sem1; UID = CC21_Team4; PWD = fBit$69162; encrypt = true; trustservercertificate = true ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<StaffDetail>(entity =>
            {
                entity.HasKey(e => e.StaffId);

                entity.Property(e => e.StaffId)
                    .ValueGeneratedNever()
                    .HasColumnName("StaffID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength(true);

                entity.Property(e => e.Subject)
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<StudentDetail>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<StudentGrade>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.M1).HasColumnName("m1");

                entity.Property(e => e.M2).HasColumnName("m2");

                entity.Property(e => e.M3).HasColumnName("m3");

                entity.Property(e => e.M4).HasColumnName("m4");

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ItemInventory>(entity =>
            {
                entity.HasKey(e => e.ItemID);

                entity.Property(e => e.ItemID).HasColumnName("ItemID");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength(true);

                entity.Property(e => e.Quantity).HasColumnName("Quantity");

                entity.Property(e => e.Price).HasColumnName("Price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
