using Autobase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autobase.DAO
{
    public interface TripDAO
    {
        List<Trip> Read();
        void Create(Trip trip);
        void Delete(Trip trip);
        void Update(Trip trip);
        Trip getTripById(int id);
    }
}