using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Lot : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ImageLink { get; set; }

        [Required]
        public string Link { get; set; }

        public string Manufacturer { get; set; }

        [Required]
        public int WeightInGrams { get; set; }

        [Required]
        public Shop Shop { get; set; }

        public List<Price> _prices;
        public List<Price> Prices { get => _prices ?? new List<Price>(); set => _prices = value; }
    }
}
