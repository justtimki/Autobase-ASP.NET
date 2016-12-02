using Autobase.DAO;
using Autobase.Models;
using Autobase.Models.enums;
using Autobase.Services;
using Autobase.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Controllers
{
    [Authorize(Roles = "DRIVER")]
    public class DriverController : Controller
    {
        [Dependency]
        public TripService tripService { get; set; }

        private bool isTripCompleted = false;

        public DriverController()
        {
            ViewBag.isTripCompleted = isTripCompleted;
        }

        public ActionResult DriverMain()
        {
            updateRelatedTrips();
            return View();
        }

        public ActionResult CompleteTrip(int tripId)
        {
            try
            {
                tripService.CompleteTrip(tripId);
                isTripCompleted = !isTripCompleted;
                updateRelatedTrips();
            }
            catch (Exception ex)
            {
                return View("DriverMain");
            }
            return View("DriverMain");
        }

        private void updateRelatedTrips()
        {
            ViewBag.relatedTrips = tripService.FindTripsRelatedTo(User.Identity.Name);
        }
    }
}