using Autobase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autobase.App_Context
{
    public static class SessionContext
    {
        public const string CurrentUserKey = "CurrentUserKey";

        public static Account GetCurrentUser()
        {
            if (HttpContext.Current.Items[CurrentUserKey] == null)
            {
                return null;
            }
            return HttpContext.Current.Items[CurrentUserKey] as Account;
        }
    }
}