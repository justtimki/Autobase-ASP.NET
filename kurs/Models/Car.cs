using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autobase.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public double CarSpeed { get; set; }
        public double CarCapacity { get; set; }
        public bool IsHealthy { get; set; }
    }
}