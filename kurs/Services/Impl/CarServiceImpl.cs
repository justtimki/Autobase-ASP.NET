using Autobase.DAO;
using Autobase.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Services.Impl
{
    public class CarServiceImpl : CarService
    {
        private AccountDAO accountDAO;
        private CarDAO carDAO;
        private TripDAO tripDAO;
        private OrderDAO orderDAO;

        [Dependency]
        public AccountDAO AccountDAO { get; set; }

        [Dependency]
        public CarDAO CarDAO { get; set; }

        public Car UpdateCar(Car car, string currentUser)
        {
            if (currentUser != null && currentUser != string.Empty)
            {
                car.CarId = (int)AccountDAO.GetAccountById(Convert.ToInt32(currentUser)).CarId;
                CarDAO.Update(car);
                return car;
            }
            throw new ArgumentException("Current user is null!");
            
        }

        public Car GetDriversCar(string currentUser)
        {
            if (currentUser != null && currentUser != string.Empty)
            {
                Account currentAccount = AccountDAO.GetAccountById(Convert.ToInt32(currentUser));
                return CarDAO.GetById((int)currentAccount.CarId);
            }
            throw new ArgumentException("Current user is null!");
        }
    }
}