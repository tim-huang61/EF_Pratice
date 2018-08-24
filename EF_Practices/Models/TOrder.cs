using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Practices.Models
{
    [Table("Orders")]
    public class TOrder
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual ICollection<TOrderDetail> OrderDetails { get; set; }
    }
}