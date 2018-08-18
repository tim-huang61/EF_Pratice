using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Practices
{
    public class TCustomer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public DateTime CreateTime { get; set; }
    }
}