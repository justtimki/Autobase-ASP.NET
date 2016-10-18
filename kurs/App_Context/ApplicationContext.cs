using Autobase.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Autobase.App_Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("Autobase")
        { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }
}