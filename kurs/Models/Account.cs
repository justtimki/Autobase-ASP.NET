using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Autobase.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public bool IsDispatcher { get; set; }
        public string Password { get; set; }
        public int? CarId { get; set; }
        public Car Car { get; set; }
    }
}