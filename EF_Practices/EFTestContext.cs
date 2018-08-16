using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace EF_Practices
{
    public class EFTestContext : DbContext
    {
        public EFTestContext() : base("EFTestDb")
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetProduct(modelBuilder);
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasColumnName("CustomerName")
                .HasMaxLength(10);
            modelBuilder.Entity<Customer>().HasIndex(c => c.Name).HasName("CustomerNameIndex").IsClustered(false);
            modelBuilder.Entity<Customer>().Property(c => c.CreateTime)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            base.OnModelCreating(modelBuilder);
        }

        private void SetProduct(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Ignore(p => p.SPrice);
            modelBuilder.Entity<Product>().Property(p => p.Money).HasColumnName("Price").HasColumnType("money");
            modelBuilder.Entity<Product>().HasKey(p => p.PId);
            modelBuilder.Entity<Product>().Property(p => p.CreateTime)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasColumnName("ProductName")
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                    new IndexAttribute("ProductName", 2) {IsUnique = true})).HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(p => p.Category).HasColumnType("NVARCHAR").HasMaxLength(25)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("CategoryIndex", 1)));
        }
    }
}