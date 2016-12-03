using Autobase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobase.Services
{
    public interface TripService
    {
        List<Trip> FindTripsRelatedTo(string currentUser);
        void CompleteTrip(int tripId);
        void CreateTrip(int orderId, int driverId);
        Trip FindTripByOrderId(int orderId);
    }
}
