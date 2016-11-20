using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models;
using Autobase.App_Context;
using System.Data.Entity;
using Microsoft.Practices.Unity;

namespace Autobase.DAO.MSSQLImpl
{
    public class MSSQLOrderDAO : OrderDAO
    {
        [Dependency]
        public ApplicationContext AppContext { get; set; }

        public void Create(Order order)
        {
            AppContext.Orders.Add(order);
            AppContext.SaveChanges();
        }

        public void Delete(Order order)
        {
            AppContext.Orders.Remove(order);
            AppContext.SaveChanges();
        }

        public Order GetOrderById(int id)
        {
            return AppContext.Orders.First(order => order.OrderId == id);
        }

        public List<Order> Read()
        {
            List<Order> orders = AppContext.Orders.ToList();
            return orders;
        }

        public void Update(Order order)
        {
            AppContext.Entry(order).State = EntityState.Modified;
            AppContext.SaveChanges();
        }
    }
}