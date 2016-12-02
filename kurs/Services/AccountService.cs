using Autobase.DAO;
using Autobase.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Autobase.Services
{
    public interface AccountService
    {
        List<Account> FindAllDrivers();
        List<Account> FindDriversSuitableFor(int orderId);
        Account FindAccountById(int accountId);
        void CreateDriverAccount(Account account);
        Account FindAccountByNameAndPassword(string login, string password);

    }
}
