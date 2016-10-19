using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models;
using Autobase.App_Context;

namespace Autobase.DAO.MSSQLImpl
{
    public class MSSQLOrderDAO : OrderDAO
    {
        readonly ApplicationContext appContext;

        public MSSQLOrderDAO(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }

        public void Create(Order order)
        {
            appContext.Orders.Add(order);
            appContext.SaveChanges();
        }

        public void Delete(Order order)
        {
            appContext.Orders.Remove(order);
            appContext.SaveChanges();
        }

        public List<Order> Read()
        {
            List<Order> orders = appContext.Orders.ToList();
            return orders;
        }

        public void Update(Order order)
        {
            
        }
    }
}