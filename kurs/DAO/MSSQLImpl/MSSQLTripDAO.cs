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
            AppContext.Entry(trip).State = EntityState.Modified;
            AppContext.SaveChanges();
        }
    }
}