namespace EF_Practices
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Test
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContactName { get; set; }
    }
}
