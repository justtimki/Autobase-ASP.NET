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
        public OrderService orderService { get; set; }

        public ActionResult RemoveOrder(int orderId)
        {
            orderService.RemoveOrder(orderId);
            return RedirectToAction("DispatcherMain", "Dispatcher");
        }
    }
}