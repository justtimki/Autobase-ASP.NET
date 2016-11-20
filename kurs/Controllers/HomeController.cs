using Autobase.DAO;
using Autobase.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Controllers
{
    public class HomeController : Controller
    {
        private AccountDAO accountDAO;

        [Dependency]
        public CarDAO CarDAO { get; set; }

        [Dependency]
        public AccountDAO AccountDAO
        {
            get
            {
                if (accountDAO == null)
                {
                    accountDAO = DependencyResolver.Current.GetService<AccountDAO>();
                }
                return accountDAO;
            }
        }

        public ActionResult Index()
        {
            CarDAO.Read();
            ViewBag.accountDAO = accountDAO;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}