using Autobase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobase.Services
{
    public interface OrderService
    {
        List<Order> FindAllOrders();

        Order FindOrderById(int orderId);

        void RemoveOrder(int orderId);
        void UpdateOrder(Order order);
    }
}
