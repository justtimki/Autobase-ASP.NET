using Autobase.App_Context;
using Autobase.DAO;
using Autobase.DAO.MSSQLImpl;
using Autobase.Models;
using System;
using System.Collections.Generic;
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
        protected void Application_Start()
        {
            // Setup DI
            UnityConfig.RegisterComponents();

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

                        ApplicationContext appContext = new ApplicationContext();
                        Account user = appContext.Accounts.FirstOrDefault(a => a.AccountId == Convert.ToInt32(id.Name));

                        HttpContext.Current.Items[SessionContext.CurrentUserKey] = user;
                        HttpContext.Current.User = new GenericPrincipal(id, new string[] {
                            (user.IsDispatcher) ? "dispathcer" : "driver"
                        });
                    }
                }
            }
        }
    }
}
