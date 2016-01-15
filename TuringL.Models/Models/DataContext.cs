namespace TuringL.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<InstallInfo> InstallInfoes { get; set; }
        public virtual DbSet<MaintanceRecord> MaintanceRecords { get; set; }
        public virtual DbSet<ProductAddtionalInfo> ProductAddtionalInfoes { get; set; }
        public virtual DbSet<ProductInfo> ProductInfoes { get; set; }
        public virtual DbSet<SysRecord> SysRecords { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstallInfo>()
                .Property(e => e.Principal)
                .IsUnicode(false);

            modelBuilder.Entity<InstallInfo>()
                .Property(e => e.MaintancePeriod)
                .IsUnicode(false);

            modelBuilder.Entity<InstallInfo>()
                .Property(e => e.Site)
                .IsUnicode(false);

            modelBuilder.Entity<InstallInfo>()
                .Property(e => e.InstallMethod)
                .IsUnicode(false);

            modelBuilder.Entity<InstallInfo>()
                .Property(e => e.CNumber)
                .IsUnicode(false);

            modelBuilder.Entity<InstallInfo>()
                .Property(e => e.ProductId)
                .IsUnicode(false);

            modelBuilder.Entity<MaintanceRecord>()
                .Property(e => e.KeyWorld)
                .IsUnicode(false);

            modelBuilder.Entity<MaintanceRecord>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<MaintanceRecord>()
                .Property(e => e.Solution)
                .IsUnicode(false);

            modelBuilder.Entity<MaintanceRecord>()
                .Property(e => e.MiantanceUser)
                .IsUnicode(false);

            modelBuilder.Entity<MaintanceRecord>()
                .Property(e => e.ProductId)
                .IsUnicode(false);

            modelBuilder.Entity<MaintanceRecord>()
                .Property(e => e.FirstPartier)
                .IsUnicode(false);

            modelBuilder.Entity<ProductAddtionalInfo>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductAddtionalInfo>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<ProductAddtionalInfo>()
                .Property(e => e.ProductId)
                .IsUnicode(false);

            modelBuilder.Entity<ProductInfo>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<ProductInfo>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProductInfo>()
                .Property(e => e.TypeVersion)
                .IsUnicode(false);

            modelBuilder.Entity<ProductInfo>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<ProductInfo>()
                .HasMany(e => e.InstallInfoes)
                .WithOptional(e => e.ProductInfo)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<ProductInfo>()
                .HasMany(e => e.MaintanceRecords)
                .WithOptional(e => e.ProductInfo)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<ProductInfo>()
                .HasMany(e => e.ProductAddtionalInfoes)
                .WithOptional(e => e.ProductInfo)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<SysRecord>()
                .Property(e => e.User)
                .IsUnicode(false);

            modelBuilder.Entity<SysRecord>()
                .Property(e => e.Context)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Duty)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.RoleId)
                .IsUnicode(false);
        }
    }
}
