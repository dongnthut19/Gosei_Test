using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Models
{
    public partial class EmployeeQualificationContext : DbContext
    {
        public EmployeeQualificationContext(DbContextOptions<EmployeeQualificationContext> options) : base (options)
        {
            
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeQualification> EmployeeQualification { get; set; }
        public virtual DbSet<Qualification> Qualification { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EmployeeQualification;Trusted_Connection=True;");
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.MiddleName).HasMaxLength(250);
            });

            modelBuilder.Entity<EmployeeQualification>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(250);

                entity.Property(e => e.Institution)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidTo).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeQualification)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeQualification_Employee");

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.EmployeeQualification)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeQualification_Qualification");
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
