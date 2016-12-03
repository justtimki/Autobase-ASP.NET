using Autobase.DAO;
using Autobase.Models;
using Autobase.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Controllers
{
    public class OrderController : Controller
    {
        [Dependency]
        public OrderService OrderService { get; set; }

        [Dependency]
        public TripService TripService { get; set; }

        public ActionResult RemoveOrder(int orderId)
        {
            OrderService.RemoveOrder(orderId);
            return RedirectToAction("DispatcherMain", "Dispatcher");
        }

        public ActionResult TripDetails(int orderId)
        {
            Trip trip = TripService.FindTripByOrderId(orderId);
            return PartialView("OrderDetails", trip);
        }

        public ActionResult OrderDetails(int orderId)
        {
            Order order = OrderService.FindOrderById(orderId);
            return PartialView("OrderDetails", order);
        }

        public ActionResult UpdateOrder(string orederId, string requiredCarSpeed, string requiredCarCapacity)
        {
            try
            {
                int id = Convert.ToInt32(orederId);
                Order order = OrderService.FindOrderById(id);
                order.RequiredCarCapacity = Convert.ToDouble(requiredCarCapacity);
                order.RequiredCarSpeed = Convert.ToDouble(requiredCarSpeed);
                OrderService.UpdateOrder(order);
                return RedirectToAction("DispatcherMain", "Dispatcher");
            } 
            catch(Exception)
            {
                return View("Error");
            }
        }
    }
}