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
        }
    }

    public class Product
    {
        [Key]
        public int PId { get; set; }

        [Required]
        [Index("ProductName", 2, IsUnique = true)]
        [Column("ProductName")]
        [MaxLength(50, ErrorMessage = "the length can not be over 50")]
        [MinLength(2, ErrorMessage = "the length can not be less 2")]
        public string Name { get; set; }

        [Column("Price", TypeName = "Money")]
        public decimal Money { get; set; }

        [NotMapped]
        public decimal SPrice { get; set; }

        /// <summary>
        /// Gets or sets the category.
        ///  EF index如果為字串則需要設定Column
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(25)]
        [Index("CategoryIndex", 1)]
        public string Category { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
    }
}