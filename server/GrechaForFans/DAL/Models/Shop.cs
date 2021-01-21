using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Shop : BaseEntity
    {
        public string Name { get; set; }

        public List<Lot> _lots;
        public List<Lot> Lots { get => _lots ?? new List<Lot>(); set => _lots = value; }
    }
}
