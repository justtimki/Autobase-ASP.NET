using Autobase.DAO;
using Autobase.Models;
using Autobase.Models.enums;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Services.Impl
{
    public class TripServiceImpl : TripService
    {
        [Dependency]
        public AccountDAO AccountDAO { get; set; }


        [Dependency]
        public TripDAO TripDAO { get; set; }

        [Dependency]
        public OrderDAO OrderDAO { get; set; }

        [Dependency]
        public CarDAO CarDAO { get; set; }

        public List<Trip> FindTripsRelatedTo(string currentUser)
        {
            List<Trip> relatedTrips = new List<Trip>();
            if (currentUser != null && currentUser != string.Empty)
            {
                relatedTrips = TripDAO.Read()
                    .Where(trip => trip.AccountId == Convert.ToInt32(currentUser)
                                   && TripStatusEnum.IN_PROCESS.Equals(OrderDAO.GetOrderById(trip.OrderId).Status))
                    .ToList();
            }

            return relatedTrips;
        }

        public void CompleteTrip(int tripId)
        {
            Trip trip = TripDAO.getTripById(tripId);
            Order completedOrder = OrderDAO.GetOrderById(trip.OrderId);
            completedOrder.Status = TripStatusEnum.DONE;
            OrderDAO.Update(completedOrder);

            TripDAO.Delete(trip);
        }

        public void CreateTrip(int orderId, int driverId)
        {
            Account driver = AccountDAO.GetAccountById(driverId);
            Order order = OrderDAO.GetOrderById(orderId);
            Car car = CarDAO.GetById((int) driver.CarId);
            driver.Car = car;
            Trip trip = new Trip();
            trip.TripDate = DateTime.Now;
            trip.OrderId = order.OrderId;
            trip.TripName = order.OrderName;
            trip.CarId = driver.Car.CarId;
            trip.AccountId = driver.AccountId;
            TripDAO.Create(trip);
            order.Status = TripStatusEnum.IN_PROCESS;
            OrderDAO.Update(order);
        }

        public Trip FindTripByOrderId(int orderId)
        {
            Order order = OrderDAO.GetOrderById(orderId);
            Trip trip = TripDAO.Read().First(t => t.OrderId == orderId);
            Car car = CarDAO.GetById(trip.CarId);
            Account account = AccountDAO.GetAccountById(trip.AccountId);
            trip.Order = order;
            trip.Account = account;
            trip.Car = car;
            return trip;
        }
    }
}