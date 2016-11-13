using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobase.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public string TripName { get; set; }
        public DateTime TripDate { get; set; }

        public int OrderId { get; set; }
        public Order Oder { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}