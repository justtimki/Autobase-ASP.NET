using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models.enums;
using Autobase.Models;
using Microsoft.Practices.Unity;
using Autobase.DAO;
using System.Web.Mvc;

namespace Autobase.Services.Filters
{
    public class FilterImpl : Filter
    {
        [Dependency]
        public OrderDAO OrderDAO { get; set; }

        public List<Order> FilterOrderByStatus(string status)
        {
            if (TripStatusEnum.DONE.ToString().Equals(status))
            {
                return SetOrdersByStatus(TripStatusEnum.DONE);
            }
            else if (TripStatusEnum.IN_PROCESS.ToString().Equals(status))
            {
                return SetOrdersByStatus(TripStatusEnum.IN_PROCESS);
            }
            else if (TripStatusEnum.WAITING.ToString().Equals(status))
            {
                return SetOrdersByStatus(TripStatusEnum.WAITING);
            }
            return OrderDAO.Read();
        }

        private List<Order> SetOrdersByStatus(TripStatusEnum status)
        {
            return OrderDAO.Read().Where(trip => status.Equals(trip.Status)).ToList();
        }
    }
}