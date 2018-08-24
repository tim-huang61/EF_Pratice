using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Practices.Models
{
    [Table("OrderDetails")]
    public class TOrderDetail
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public virtual TOrder Order { get; set; }
    }
}