using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models;
using Autobase.App_Context;
using System.Data.Entity;

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
            Order orderToChange = appContext.Orders.First(o => order.OrderId == o.OrderId);
            if (orderToChange == null)
            {
                throw new ArgumentException("Order with id " + order.OrderId + " not found.");
            }

            orderToChange.OrderName = order.OrderName;
            orderToChange.RequiredCarCapacity = order.RequiredCarCapacity;
            orderToChange.RequiredCarSpeed = order.RequiredCarSpeed;
            orderToChange.Status = order.Status;

            appContext.Entry(orderToChange).State = EntityState.Modified;
            appContext.SaveChanges();
        }
    }
}