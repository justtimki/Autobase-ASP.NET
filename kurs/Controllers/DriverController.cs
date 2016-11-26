using Autobase.DAO;
using Autobase.Models;
using Autobase.Models.enums;
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
        private AccountDAO accountDAO;
        private CarDAO carDAO;
        private TripDAO tripDAO;
        private OrderDAO orderDAO;

        [Dependency]
        public AccountDAO AccountDAO
        {
            get
            {
                if (accountDAO == null)
                {
                    accountDAO = DependencyResolver.Current.GetService<AccountDAO>();
                }
                return accountDAO;
            }
        }

        [Dependency]
        public CarDAO CarDAO
        {
            get
            {
                if (carDAO == null)
                {
                    carDAO = DependencyResolver.Current.GetService<CarDAO>();
                }
                return carDAO;
            }
        }

        [Dependency]
        public TripDAO TripDAO
        {
            get
            {
                if (tripDAO == null)
                {
                    tripDAO = DependencyResolver.Current.GetService<TripDAO>();
                }
                return tripDAO;
            }
        }

        [Dependency]
        public OrderDAO OrderDAO
        {
            get
            {
                if (orderDAO == null)
                {
                    orderDAO = DependencyResolver.Current.GetService<OrderDAO>();
                }
                return orderDAO;
            }
        }

        private List<Trip> relatedTrips = new List<Trip>();
        private bool isTripCompleted = false;

        public DriverController()
        {
            updateRelatedTrips();
            ViewBag.isTripCompleted = isTripCompleted;
        }

        // GET: Driver
        public ActionResult DriverMain()
        {
            return View();
        }

        public ActionResult CompleteTrip(int tripId)
        {
            try
            {
                Trip trip = TripDAO.getTripById(tripId);
                Order completedOrder = OrderDAO.GetOrderById(trip.OrderId);
                completedOrder.Status = TripStatusEnum.DONE;
                OrderDAO.Update(completedOrder);

                TripDAO.Delete(trip);
                isTripCompleted = !isTripCompleted;
                updateRelatedTrips();
            }
            catch (Exception e)
            {
                return View("DriverMain");
            }
            return View("DriverMain");
        }

        private void updateRelatedTrips()
        {
            relatedTrips = TripDAO.Read()
                .Where(trip => trip.AccountId == Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name)
                               && TripStatusEnum.PERFORMED.Equals(OrderDAO.GetOrderById(trip.OrderId).Status))
                .ToList();
            ViewBag.relatedTrips = relatedTrips;
        }
    }
}