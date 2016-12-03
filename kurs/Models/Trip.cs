using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Autobase.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        public string TripName { get; set; }
        public DateTime TripDate { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}