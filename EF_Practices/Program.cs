using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;

namespace EF_Practices
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var context = new EFTestContext();
            var products = context.Products.ToList();

            context.SaveChanges();

            Console.Read();
        }
    }

    public class EFTestContext : DbContext
    {
        public EFTestContext() : base("EFTestDb")
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Ignore(p => p.SPrice);
            modelBuilder.Entity<Product>().Property(p => p.Money).HasColumnName("Price").HasColumnType("money");
            modelBuilder.Entity<Product>().HasKey(p => p.PId);
            modelBuilder.Entity<Product>().Property(p => p.CreateTime)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasColumnName("ProductName")
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                    new IndexAttribute("ProductName", 2) { IsUnique = true })).HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(p => p.Category).HasColumnType("NVARCHAR").HasMaxLength(25)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("CategoryIndex", 1)));

            base.OnModelCreating(modelBuilder);
        }
    }

    public class Product
    {
        public int PId { get; set; }

        [MaxLength(50, ErrorMessage = "the length can not be over 50")]
        [MinLength(2, ErrorMessage = "the length can not be less 2")]
        public string Name { get; set; }

        public decimal Money { get; set; }

        public decimal SPrice { get; set; }

        /// <summary>
        /// Gets or sets the category.
        ///  EF index如果為字串則需要設定Column
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        public DateTime CreateTime { get; set; }
    }
}