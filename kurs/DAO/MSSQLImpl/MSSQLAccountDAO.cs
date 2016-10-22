using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models;
using Autobase.App_Context;
using System.Data.Entity;

namespace Autobase.DAO.MSSQLImpl
{
    public class MSSQLAccountDAO : AccountDAO
    {
        readonly ApplicationContext appContext;

        public MSSQLAccountDAO(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }

        public void Create(Account account)
        {
            appContext.Accounts.Add(account);
            appContext.SaveChanges();
        }

        public void Delete(Account account)
        {
            appContext.Accounts.Remove(account);
            appContext.SaveChanges();
        }

        public Account GetAccountByNameAndPass(string accountName, string password)
        {
            Account acc = appContext.Accounts.FirstOrDefault(a =>
                a.AccountName.Equals(accountName) && a.Password.Equals(password));

            return acc;
        }

        public List<Account> Read()
        {
            List<Account> accounts = appContext.Accounts.ToList();
            return accounts;
        }

        public void Update(Account account)
        {
            Account accToChange = appContext.Accounts.First(acc => acc.AccountId == account.AccountId);
            if (accToChange == null)
            {
                throw new ArgumentException("Account with id " + account.AccountId + " not found.");
            }

            accToChange.AccountName = account.AccountName;
            accToChange.Car = account.Car;
            accToChange.Role = account.Role;
            accToChange.Password = account.Password;
            accToChange.CarId = account.CarId;

            appContext.Entry(accToChange).State = EntityState.Modified;
            appContext.Accounts.Add(accToChange);
        }
    }
}