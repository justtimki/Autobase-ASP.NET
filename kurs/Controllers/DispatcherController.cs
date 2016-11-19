using Autobase.App_Context;
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
    public class DispatcherController : Controller
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

        private List<Account> driversAccounts = new List<Account>();
        private List<Order> orders = new List<Order>();
        private List<Account> suitableAccounts = new List<Account>();
        private bool isTripCreated = false;

        public DispatcherController()
        {
            driversAccounts = AccountDAO.Read().Where(acc => Role.DRIVER.Equals(acc.Role)).ToList();
            driversAccounts.ForEach(acc => acc.Car = CarDAO.GetById((int)acc.CarId));
            orders = OrderDAO.Read();
            ViewBag.driversAccounts = driversAccounts;
            ViewBag.orders = orders;
        }

        // GET: Dispatcher
        public ActionResult DispatcherMain()
        {          
            return View();
        }

        public ActionResult ChooseDriver(int orderId)
        {
            Order order = OrderDAO.GetOrderById(orderId);
            List<Account> suitableAccounts = driversAccounts
                .Where(acc =>
                            acc.Car.CarCapacity >= order.RequiredCarCapacity
                            && acc.Car.CarSpeed >= order.RequiredCarSpeed
                            && acc.Car.IsHealthy)
                .ToList();
            ViewBag.suitableAccounts = suitableAccounts;
            return View();
        }

        public ActionResult CreateTrip(int orderId, int driverId)
        {
            Account driver = AccountDAO.GetAccountById(driverId);
            Order order = OrderDAO.GetOrderById(orderId);
            CreateTrip(driver, order);
            
            return View("DispatcherMain");
        }


        private void CreateTrip(Account driver, Order order)
        {
            Trip trip = new Trip();
            trip.TripDate = DateTime.Now;
            trip.Oder = order;
            trip.OrderId = order.OrderId;
            trip.TripName = order.OrderName;
            trip.Car = driver.Car;
            trip.CarId = driver.Car.CarId;
            trip.Account = driver;
            trip.AccountId = driver.AccountId;
            isTripCreated = !isTripCreated;
            TripDAO.Create(trip);

            ViewBag.isTripCreated = isTripCreated;
        }
    }
}
