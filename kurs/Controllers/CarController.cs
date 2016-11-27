using Autobase.DAO;
using Autobase.Models;
using Autobase.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Controllers
{
    public class CarController : Controller
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
        
        public ActionResult CarDetails()
        {
            SetCurrentAccountCar();
            return View("CarDetails");
        }

        public ActionResult UpdateCar(Car car)
        {
            if (verifyCar(car))
            {
                car.CarId = (int) AccountDAO.GetAccountById(Convert.ToInt32(User.Identity.Name)).CarId;
                ViewBag.car = car;
                CarDAO.Update(car);
            }
            else
            {
                SetCurrentAccountCar();
            }
            return View("CarDetails");
        }

        private void SetCurrentAccountCar()
        {
            Account currentAccount = AccountDAO.GetAccountById(Convert.ToInt32(User.Identity.Name));
            ViewBag.car = CarDAO.GetById((int)currentAccount.CarId);
        }

        private bool verifyCar(Car car)
        {
            bool isValid = true;
            if (!ValidationUtil.isNameValid(car.CarName))
            {
                ModelState.AddModelError("CarName", "Car Name shouldn't be empty or contains illegal chars.");
                isValid = false;
            }
            if (!ValidationUtil.isNumberValid(car.CarCapacity))
            {
                ModelState.AddModelError("CarCapacity", "Car Capacity should be more than 0");
                isValid = false;
            }
            if (!ValidationUtil.isNumberValid(car.CarSpeed))
            {
                ModelState.AddModelError("CarCapacity", "Car Speed should be more than 0");
                isValid = false;
            }

            return isValid;
        }
    }
}