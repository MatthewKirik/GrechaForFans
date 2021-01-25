using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.Filters
{
    public class LotFilter
    {
        public int? FromWeight { get; set; }
        public int? ToWeight { get; set; }

        public int? ShopId { get; set; }

        public int Limit { get; set; }

        public string Order { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
