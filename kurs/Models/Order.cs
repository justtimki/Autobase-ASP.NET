using kurs.Models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace kurs.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public double RequiredCarSpeed { get; set; }
        public double RequiredCarCapacity { get; set; }
        public TripStatusEnum status { get; set; }
    }
}