using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Autobase.App_Context;
using Autobase.DAO;
using Autobase.DAO.MSSQLImpl;
using System.Data.Entity;
using Autobase.Services;
using Autobase.Services.Filters;
using Autobase.Services.Impl;

namespace Autobase
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            
            container.RegisterType(typeof(ApplicationContext));
            container.RegisterType<CarDAO, MSSQLCarDAO>();
            container.RegisterType<AccountDAO, MSSQLAccountDAO>();
            container.RegisterType<OrderDAO, MSSQLOrderDAO>();
            container.RegisterType<TripDAO, MSSQLTripDAO>();
            container.RegisterType<AccountService, AccountServiceImpl>();
            container.RegisterType<CarService, CarServiceImpl>();
            container.RegisterType<OrderService, OrderServiceImpl>();
            container.RegisterType<TripService, TripServiceImpl>();
            container.RegisterType<Services.Filters.Filter, FilterImpl>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}