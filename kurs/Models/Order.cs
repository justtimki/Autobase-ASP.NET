using Autobase.Models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobase.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public double RequiredCarSpeed { get; set; }
        public double RequiredCarCapacity { get; set; }
        public TripStatusEnum Status { get; set; }
    }
}