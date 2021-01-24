using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Price : BaseEntity
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Value { get; set; }

        public Lot Lot { get; set; }
    }
}
