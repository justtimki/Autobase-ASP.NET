using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models;
using Autobase.App_Context;

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
            throw new NotImplementedException();
        }
    }
}