using Autobase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autobase.DAO
{
    public interface AccountDAO
    {
        List<Account> Read();
        void Create(Account account);
        void Delete(Account account);
        void Update(Account account);
    }
}