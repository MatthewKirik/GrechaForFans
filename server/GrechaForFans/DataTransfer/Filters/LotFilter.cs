using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.Filters
{
    public class LotFilter
    {
        public int? WeightInGrams { get; set; }

        public int? ShopId { get; set; }

        public int Limit { get; set; }
    }
}
