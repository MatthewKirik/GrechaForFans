using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer
{
    public class LotDto : BaseDto
    {
        public string Title { get; set; }

        public string ImageLink { get; set; }

        public string Link { get; set; }

        public string Manufacturer { get; set; }

        public int WeightInGrams { get; set; }

        /// <summary>
        /// Optional
        /// </summary>
        public ShopDto Shop { get; set; }
    }
}
