using Autobase.App_Context;
using Autobase.DAO;
using Autobase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Authenticate(string login, string password)
        {
            Account acc = accountDao.GetAccountByNameAndPass(login, password);
            if (acc == null)
            {
                ModelState.AddModelError("Wrong input", Resources.Resource.WrongLoginOrPassword);
                return View();
            }
            else
            {
                CreateAuthCookie(acc);
                return RedirectToAction("Index", "Home");
            }
        }

        private void CreateAuthCookie(Account acc)
        {
            throw new NotImplementedException();
        }
    }
}