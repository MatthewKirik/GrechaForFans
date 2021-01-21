using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer
{
    public class PriceDto : BaseDto
    {
        public DateTime Date { get; set; }

        public decimal Value { get; set; }
    }
}
