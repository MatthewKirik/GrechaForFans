using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Price : BaseEntity
    {
        public DateTime Date { get; set; }

        public decimal Value { get; set; }

        public Lot Lot { get; set; }
    }
}
