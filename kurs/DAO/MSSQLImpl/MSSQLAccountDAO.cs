using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models;
using Autobase.App_Context;
using System.Data.Entity;
using Microsoft.Practices.Unity;

namespace Autobase.DAO.MSSQLImpl
{
    public class MSSQLAccountDAO : AccountDAO
    {
        [Dependency]
        public ApplicationContext AppContext { get; set; }

        public void Create(Account account)
        {
            AppContext.Accounts.Add(account);
            AppContext.SaveChanges();
        }

        public void Delete(Account account)
        {
            AppContext.Accounts.Remove(account);
            AppContext.SaveChanges();
        }

        public Account GetAccountById(int id)
        {
            return AppContext.Accounts.First(acc => acc.AccountId == id);
        }

        public Account GetAccountByNameAndPass(string accountName, string password)
        {
            Account acc = AppContext.Accounts.FirstOrDefault(a =>
                a.AccountName.Equals(accountName) && a.Password.Equals(password));

            return acc;
        }

        public List<Account> Read()
        {
            List<Account> accounts = AppContext.Accounts.ToList();
            return accounts;
        }

        public void Update(Account account)
        {
            AppContext.Entry(account).State = EntityState.Modified;
            AppContext.SaveChanges();
        }
    }
}