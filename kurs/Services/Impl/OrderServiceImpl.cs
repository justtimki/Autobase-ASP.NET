using Autobase.DAO;
using Autobase.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Services.Impl
{
    public class OrderServiceImpl : OrderService
    {
        [Dependency]
        public OrderDAO OrderDAO { get; set; }

        public List<Order> FindAllOrders()
        {
            return OrderDAO.Read();
        }

        public Order FindOrderById(int orderId)
        {
            return OrderDAO.GetOrderById(orderId);
        }

        public void RemoveOrder(int orderId)
        {
            OrderDAO.Delete(OrderDAO.GetOrderById(orderId));
        }

    }
}