using Autobase.DAO;
using Autobase.Models;
using Autobase.Models.enums;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Services.Impl
{
    public class AccountServiceImpl : AccountService
    {
        [Dependency]
        public AccountDAO AccountDAO { get; set; }

        [Dependency]
        public CarDAO CarDAO { get; set; }

        [Dependency]
        public OrderDAO OrderDAO { get; set; }

        public List<Account> FindAllDrivers()
        {
            List<Account> driversAccounts = new List<Account>();
            driversAccounts = AccountDAO.Read().Where(acc => Role.DRIVER.Equals(acc.Role)).ToList();
            driversAccounts.ForEach(acc => acc.Car = CarDAO.GetById((int)acc.CarId));
            return driversAccounts;
        }

        public List<Account> FindDriversSuitableFor(int orderId)
        {
            Order order = OrderDAO.GetOrderById(orderId);
            List<Account> suitableAccounts = FindAllDrivers()
                .Where(acc =>
                            acc.Car.CarCapacity >= order.RequiredCarCapacity
                            && acc.Car.CarSpeed >= order.RequiredCarSpeed
                            && acc.Car.IsHealthy)
                .ToList();

            return suitableAccounts;
        }

        public Account FindAccountById(int accountId)
        {
            return AccountDAO.GetAccountById(accountId);
        }

        public void CreateDriverAccount(Account account)
        {
            AccountDAO.Create(account);
        }

        public Account FindAccountByNameAndPassword(string login, string password)
        {
            return AccountDAO.GetAccountByNameAndPass(login, password);
        }
    }
}