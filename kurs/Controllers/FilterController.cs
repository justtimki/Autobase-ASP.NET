using Autobase.DAO;
using Autobase.Models.enums;
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
        private AccountDAO accountDAO;
        private CarDAO carDAO;
        private TripDAO tripDAO;
        private OrderDAO orderDAO;
        private const string ORDERS_KEY_TEMP_DATA = "orders";
        private const string FILTER_STATUS_DISABLED = "DISABLED";

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

        // GET: Filter
        public ActionResult FilterByState(string status)
        {
            if (TripStatusEnum.DONE.ToString().Equals(status))
            {
                SetOrdersByStatus(TripStatusEnum.DONE);
            }
            else if (TripStatusEnum.IN_PROCESS.ToString().Equals(status))
            {
                SetOrdersByStatus(TripStatusEnum.IN_PROCESS);
            }
            else if (TripStatusEnum.WAITING.ToString().Equals(status))
            {
                SetOrdersByStatus(TripStatusEnum.WAITING);
            }
            else if (FILTER_STATUS_DISABLED.Equals(status))
            {
                TempData.Remove(ORDERS_KEY_TEMP_DATA);
            }
            return RedirectToAction("DispatcherMain", "Dispatcher");
        }

        private void SetOrdersByStatus(TripStatusEnum status)
        {
            if (TempData.ContainsKey(ORDERS_KEY_TEMP_DATA))
            {
                TempData.Remove(ORDERS_KEY_TEMP_DATA);
            }
            TempData.Add(ORDERS_KEY_TEMP_DATA, OrderDAO.Read().Where(trip => status.Equals(trip.Status)).ToList());
        }
    }
}