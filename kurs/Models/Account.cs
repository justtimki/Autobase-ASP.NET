using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Autobase.Models.enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Autobase.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        public int? CarId { get; set; }
        public Car Car { get; set; }
    }
}