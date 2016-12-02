using Autobase.App_Context;
using Autobase.DAO;
using Autobase.Models;
using Autobase.Models.enums;
using Autobase.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Controllers
{
    [Authorize(Roles = "DISPATCHER")]
    public class DispatcherController : Controller
    {
        [Dependency]
        public AccountService accountService { get; set; }

        [Dependency]
        public OrderService orderService { get; set; }

        [Dependency]
        public TripService tripService { get; set; }

        private bool isTripCreated = false;

        public ActionResult DispatcherMain()
        {
            UpdateDrivers();
            UpdateOrders();
            return View();
        }

        public ActionResult ChooseDriver(int orderId)
        {
            ViewBag.suitableAccounts = accountService.FindDriversSuitableFor(orderId); ;
            return View();
        }

        public ActionResult CreateTrip(int orderId, int driverId)
        {
            try
            {
                tripService.CreateTrip(orderId, driverId);
                ViewBag.isTripCreated = !isTripCreated;
                UpdateDrivers();
                UpdateOrders();
                return View("DispatcherMain");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        private void UpdateDrivers()
        {
            ViewBag.driversAccounts = accountService.FindAllDrivers();
        }

        private void UpdateOrders()
        {
            ViewBag.orders = orderService.FindAllOrders();
        }
    }
}
