using Autobase.DAO;
using Autobase.Models;
using Autobase.Models.enums;
using Autobase.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.App_Context
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        private const int ROW_COUNT = 100;
        private CarDAO carDAO;
        private AccountDAO accountDAO;
        private OrderDAO orderDAO;
        private TripDAO tripDAO;

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

        protected override void Seed(ApplicationContext context)
        {
            FillTables();
        }

        protected void FillTables()
        {
            CreateCars();
            CreateAccounts();
            CreateOrders();
            CreateTrips();
        }

        private void CreateCars()
        {
            for (int i = 0; i < ROW_COUNT; i++)
            {
                Car car = new Car();
                car.CarName = RandomUtil.GetInstance.GetRandomString;
                car.CarSpeed = 100 * RandomUtil.GetInstance.GetRandomDouble;
                car.CarCapacity = 100 * RandomUtil.GetInstance.GetRandomDouble;
                car.IsHealthy = RandomUtil.GetInstance.GetRandomBool;
                CarDAO.Create(car);
            }
        }

        private void CreateAccounts()
        {
            for (int i = 0; i < ROW_COUNT; i++)
            {
                Account acc = new Account();
                acc.AccountName = RandomUtil.GetInstance.GetRandomString;
                acc.Role = RandomUtil.GetInstance.GetRandomBool ? Role.DISPATHCER : Role.DRIVER;
                acc.Password = RandomUtil.GetInstance.GetRandomString;
                if (Role.DRIVER.Equals(acc.Role))
                {
                    acc.Car = CarDAO.GetById(RandomUtil.GetInstance.GetRandomId);
                }
                AccountDAO.Create(acc);
            }
        }

        private void CreateOrders()
        {
            Order order = null;
            for (int i = 0; i < ROW_COUNT; i++)
            {
                order = new Order();
                order.OrderName = RandomUtil.GetInstance.GetRandomString;
                order.RequiredCarCapacity = RandomUtil.GetInstance.GetRandomDouble;
                order.RequiredCarSpeed = RandomUtil.GetInstance.GetRandomDouble;
                order.Status = RandomUtil.GetInstance.GetRandomStatus;
                OrderDAO.Create(order);
            }
        }

        private void CreateTrips()
        {
            Trip trip = null;
            for (int i = 0; i < ROW_COUNT; i++)
            {
                trip = new Trip();
                trip.Oder = OrderDAO.GetOrderById(RandomUtil.GetInstance.GetRandomId);
                Account acc;
                int j = 0;
                do
                {
                    acc = AccountDAO.GetAccountById(RandomUtil.GetInstance.GetRandomId);
                    j++;
                }
                while (acc.Car.CarSpeed <= trip.Oder.RequiredCarSpeed && acc.Car.CarCapacity <= trip.Oder.RequiredCarCapacity || j < 150);
                trip.Account = acc;
                trip.Car = trip.Account.Car;
                trip.TripName = RandomUtil.GetInstance.GetRandomString;
                trip.TripDate = RandomUtil.GetInstance.GetRandomDate;
                TripDAO.Create(trip);
            }
        }
    }
}