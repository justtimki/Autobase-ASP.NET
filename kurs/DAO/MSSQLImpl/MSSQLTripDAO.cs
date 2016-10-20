using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models;
using Autobase.App_Context;
using System.Data.Entity;

namespace Autobase.DAO.MSSQLImpl
{
    public class MSSQLTripDAO : TripDAO
    {
        readonly ApplicationContext appContext;

        public MSSQLTripDAO(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }

        public void Create(Trip trip)
        {
            appContext.Trips.Add(trip);
            appContext.SaveChanges();
        }

        public void Delete(Trip trip)
        {
            appContext.Trips.Remove(trip);
            appContext.SaveChanges();
        }

        public List<Trip> Read()
        {
            List<Trip> trips = appContext.Trips.ToList();
            return trips;
        }

        public void Update(Trip trip)
        {
            Trip tripToChange = appContext.Trips.First(t => trip.TripId == t.TripId);
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

            appContext.Entry(tripToChange).State = EntityState.Modified;
            appContext.SaveChanges(); 
        }
    }
}