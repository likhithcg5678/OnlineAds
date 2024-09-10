using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineAdsManagementSystem.Models
{
    public partial class OnlineAdsDbContext : DbContext
    {
        public OnlineAdsDbContext()
        {
        }

        public OnlineAdsDbContext(DbContextOptions<OnlineAdsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ad> Ads { get; set; } = null!;
        public virtual DbSet<AdImage> AdImages { get; set; } = null!;
        public virtual DbSet<Interest> Interests { get; set; } = null!;
        public virtual DbSet<MainCategory> MainCategories { get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>(entity =>
            {
                entity.ToTable("Ad");

                entity.Property(e => e.AdId).HasColumnName("AdID");

                entity.Property(e => e.AdDescription).HasColumnType("text");

                entity.Property(e => e.AdTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MainCategoryId).HasColumnName("MainCategoryID");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.MainCategory)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.MainCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ad__MainCategory__74AE54BC");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ad__SubCategoryI__75A278F5");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ad__UserID__73BA3083");
            });

            modelBuilder.Entity<AdImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__AdImages__7516F4EC54DA90BE");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.AdId).HasColumnName("AdID");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ImageURL");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.AdImages)
                    .HasForeignKey(d => d.AdId)
                    .HasConstraintName("FK__AdImages__AdID__7D439ABD");
            });

            modelBuilder.Entity<Interest>(entity =>
            {
                entity.ToTable("Interest");

                entity.Property(e => e.InterestId).HasColumnName("InterestID");

                entity.Property(e => e.AdId).HasColumnName("AdID");

                entity.Property(e => e.InterestMessage).HasColumnType("text");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.Interests)
                    .HasForeignKey(d => d.AdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Interest__AdID__797309D9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Interests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Interest__UserID__7A672E12");
            });

            modelBuilder.Entity<MainCategory>(entity =>
            {
                entity.ToTable("MainCategory");

                entity.Property(e => e.MainCategoryId).HasColumnName("MainCategoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory");

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.MainCategoryId).HasColumnName("MainCategoryID");

                entity.Property(e => e.SubCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MainCategory)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.MainCategoryId)
                    .HasConstraintName("FK__SubCatego__MainC__5EBF139D");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.City)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.FullName)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('U')")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
