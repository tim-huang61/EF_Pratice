using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Practices.Models
{
    [Table("Books")]
    public class TBook
    {
        [Key]
        public int Id { get; set; }

        public int Page { get; set; }

        public virtual TProduct Product { get; set; }
    }
}