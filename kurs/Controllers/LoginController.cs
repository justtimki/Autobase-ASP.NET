using Autobase.App_Context;
using Autobase.DAO;
using Autobase.Models;
using Autobase.Models.enums;
using Autobase.Services;
using Autobase.Utils;
using Microsoft.Practices.Unity;
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
        [Dependency]
        public AccountService accountService { get; set; }

        public ActionResult Index()
        {
            return View("LoginPage");
        }

        public ActionResult GetCurrentUserName()
        {
            return new ContentResult {
                Content = accountService.FindAccountById(Convert.ToInt32(User.Identity.Name.ToString())).AccountName
            };
        }

        public ActionResult Registration()
        {
            return View("Registration");
        }

        public ActionResult DoRegistration(Account account, Car car)
        {
            account.Car = car;
            account.Role = Role.DRIVER;
            if (VerifyUser(account))
            {
                accountService.CreateDriverAccount(account);
                CreateAuthCookie(account);
                return RedirectToAction("Index", "Home");
            }

            return View("Registration");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Authenticate(string login, string password)
        {
            Account acc = accountService.FindAccountByNameAndPassword(login, password);
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

        private bool VerifyUser(Account account)
        {
            bool isValid = true;
            if (!ValidationUtil.isNameValid(account.AccountName))
            {
                ModelState.AddModelError("AccountName", "Name shouldn't be empty or contains illegal chars.");
                isValid = false;
            }
            if (!ValidationUtil.isPasswordValid(account.Password))
            {
                ModelState.AddModelError("Password", "Password shouldn't be empty or shorter than 3 chars.");
                isValid = false;
            }
            if (!ValidationUtil.isNameValid(account.Car.CarName))
            {
                ModelState.AddModelError("CarName", "Car Name shouldn't be empty or contains illegal chars.");
                isValid = false;
            }
            if (!ValidationUtil.isNumberValid(account.Car.CarCapacity))
            {
                ModelState.AddModelError("CarCapacity", "Car Capacity should be more than 0");
                isValid = false;
            }
            if (!ValidationUtil.isNumberValid(account.Car.CarSpeed))
            {
                ModelState.AddModelError("CarCapacity", "Car Speed should be more than 0");
                isValid = false;
            }
            return isValid;
        }
    }
}