using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Lot : BaseEntity
    {
        public string Title { get; set; }

        public string ImageLink { get; set; }

        public string Link { get; set; }

        public string Manufacturer { get; set; }

        public int WeightInGrams { get; set; }

        public Shop Shop { get; set; }

        public List<Price> _prices;
        public List<Price> Prices { get => _prices ?? new List<Price>(); set => _prices = value; }
    }
}
