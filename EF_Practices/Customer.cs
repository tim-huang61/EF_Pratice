using System;

namespace EF_Practices
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public DateTime CreateTime { get; set; }
    }
}