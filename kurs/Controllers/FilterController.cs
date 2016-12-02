using Autobase.DAO;
using Autobase.Models;
using Autobase.Models.enums;
using Autobase.Services.Filters;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Controllers
{
    [Authorize(Roles = "DISPATCHER")]
    public class FilterController : Controller
    {
        [Dependency]
        public Services.Filters.Filter Filter { get; set; }

        private const string ORDERS_KEY_TEMP_DATA = "orders";
        
        public ActionResult FilterByState(string status)
        {
            List<Order> filteredOrders = Filter.FilterOrderByStatus(status);
            TempData.Remove(ORDERS_KEY_TEMP_DATA);
            if (filteredOrders.Count >= 0)
            {
                TempData.Add(ORDERS_KEY_TEMP_DATA, filteredOrders);
            }
            else
            {

            }
            return RedirectToAction("DispatcherMain", "Dispatcher");
        }
    }
}