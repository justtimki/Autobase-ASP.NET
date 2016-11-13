using Autobase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autobase.DAO
{
    public interface OrderDAO
    {
        List<Order> Read();
        void Create(Order order);
        void Delete(Order order);
        void Update(Order order);
        Order GetOrderById(int id);
    }
}