﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace EF_Practices
{
    public class EFTestContext : DbContext
    {
        public EFTestContext() : base("EFTestDb")
        {
        }

        public virtual DbSet<TProduct> Products { get; set; }

        public virtual DbSet<TCustomer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetProduct(modelBuilder);
            SetCustmer(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SetCustmer(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TCustomer>().ToTable("Customers");
            modelBuilder.Entity<TCustomer>().HasKey(c => c.Id);
            modelBuilder.Entity<TCustomer>().Property(c => c.Name).IsRequired().HasColumnName("CustomerName").HasMaxLength(10);
            modelBuilder.Entity<TCustomer>().HasIndex(c => c.Name).HasName("CustomerNameIndex").IsClustered(false);
            modelBuilder.Entity<TCustomer>().Property(c => c.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }

        /// <summary>
        /// Sets the product.如果從原本綁屬性的轉為使用Fluent API，
        /// Index要是有綁order，要使用HasColumnAnnotation、IndexAnnotation、IndexAttribute
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        private void SetProduct(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TProduct>().ToTable("Products");
            modelBuilder.Entity<TProduct>().Ignore(p => p.SPrice);
            modelBuilder.Entity<TProduct>().Property(p => p.Money).HasColumnName("Price").HasColumnType("money");
            modelBuilder.Entity<TProduct>().HasKey(p => p.PId);
            modelBuilder.Entity<TProduct>().Property(p => p.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder.Entity<TProduct>().Property(p => p.Name).IsRequired().HasColumnName("ProductName") .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("ProductName", 2) {IsUnique = true}));
            modelBuilder.Entity<TProduct>().Property(p => p.Category).HasColumnType("NVARCHAR").HasMaxLength(25)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("CategoryIndex", 1)));
        }
    }
}