using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models;
using Autobase.App_Context;
using System.Data.Entity;
using Microsoft.Practices.Unity;

namespace Autobase.DAO.MSSQLImpl
{
    public class MSSQLTripDAO : TripDAO
    {
        [Dependency]
        public ApplicationContext AppContext { get; set; }

        public void Create(Trip trip)
        {
            AppContext.Trips.Add(trip);
            AppContext.SaveChanges();
        }

        public void Delete(Trip trip)
        {
            AppContext.Trips.Remove(trip);
            AppContext.SaveChanges();
        }

        public Trip getTripById(int id)
        {
            return AppContext.Trips.First(trip => trip.TripId == id);
        }

        public List<Trip> Read()
        {
            List<Trip> trips = AppContext.Trips.ToList();
            return trips;
        }

        public void Update(Trip trip)
        {
            Trip tripToChange = AppContext.Trips.First(t => trip.TripId == t.TripId);
            if (tripToChange == null)
            {
                throw new ArgumentException("Trip with id " + trip.TripId + " not found.");
            }

            tripToChange.Oder = trip.Oder;
            tripToChange.OrderId = trip.OrderId;
            tripToChange.Car = trip.Car;
            tripToChange.CarId = trip.CarId;
            tripToChange.Account = trip.Account;
            tripToChange.AccountId = trip.AccountId;
            tripToChange.TripDate = trip.TripDate;
            tripToChange.TripName = trip.TripName;

            AppContext.Entry(tripToChange).State = EntityState.Modified;
            AppContext.SaveChanges(); 
        }
    }
}