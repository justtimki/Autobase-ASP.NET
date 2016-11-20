using Autobase.App_Context;
using Autobase.DAO;
using Autobase.DAO.MSSQLImpl;
using Autobase.Models;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Autobase
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ApplicationContext appContext;

        public ApplicationContext AppContext
        {
            get
            {
                if (appContext == null)
                {
                    appContext = DependencyResolver.Current.GetService<ApplicationContext>();
                }
                return appContext;
            }
        }

        protected void Application_Start()
        {
            // Setup DI
            UnityConfig.RegisterComponents();
            
            Database.SetInitializer(new DatabaseInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id =
                            (FormsIdentity)HttpContext.Current.User.Identity;

                        int accId = Convert.ToInt32(id.Name);

                        Account user = AppContext.Accounts.FirstOrDefault(a => a.AccountId == accId);

                        HttpContext.Current.Items[SessionContext.CurrentUserKey] = user;
                        HttpContext.Current.User = new GenericPrincipal(id, new string[] { user.Role.ToString() });
                    }
                }
            }
        }
    }
}
