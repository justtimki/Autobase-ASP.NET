using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Autobase.App_Context;
using Autobase.DAO;
using Autobase.DAO.MSSQLImpl;
using System.Data.Entity;

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
            //container.RegisterType<ApplicationContext>();
            container.RegisterType<CarDAO, MSSQLCarDAO>();
            container.RegisterType<AccountDAO, MSSQLAccountDAO>();
            container.RegisterType<OrderDAO, MSSQLOrderDAO>();
            container.RegisterType<TripDAO, MSSQLTripDAO>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}