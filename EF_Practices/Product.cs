using System;
using System.ComponentModel.DataAnnotations;

namespace EF_Practices
{
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