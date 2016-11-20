using Autobase.App_Context;
using Autobase.DAO;
using Autobase.Models;
using Autobase.Models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Autobase.Controllers
{
    public class LoginController : Controller
    {
        readonly AccountDAO accountDao;

        public LoginController(AccountDAO accountDao)
        {
            this.accountDao = accountDao;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View("LoginPage");
        }

        public ActionResult GetCurrentUserName()
        {
            return new ContentResult { Content = accountDao.GetAccountById(Convert.ToInt32(User.Identity.Name.ToString())).AccountName };
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Authenticate(string login, string password)
        {
            Account acc = accountDao.GetAccountByNameAndPass(login, password);
            if (acc == null)
            {
                ModelState.AddModelError("Wrong input", Resources.Resource.WrongLoginOrPassword);
                return View("LoginPage");
            }
            else
            {
                CreateAuthCookie(acc);
                if (Role.DISPATCHER.Equals(acc.Role))
                {
                    return RedirectToAction("DispatcherMain", "Dispatcher");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        private void CreateAuthCookie(Account acc)
        {
            DateTime timeoutCookie = DateTime.Now.AddMinutes(15);
            bool persistentCookie = false;

            var ticket = new FormsAuthenticationTicket(1, acc.AccountId.ToString(), DateTime.Now,
                                            timeoutCookie, persistentCookie, acc.Role.ToString());
            var encTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            cookie.Expires = timeoutCookie;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(ticket), new string[] { acc.Role.ToString() });
        }
    }
}