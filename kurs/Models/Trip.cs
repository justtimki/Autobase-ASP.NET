using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Autobase.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public string TripName { get; set; }
        public DateTime TripDate { get; set; }
        public Order Oder { get; set; }
        public Account Account { get; set; }
        public Car Car { get; set; }
    }
}