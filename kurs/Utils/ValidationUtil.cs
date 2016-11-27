using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Autobase.Utils
{
    public static class ValidationUtil
    {
        public static bool isNameValid(string name)
        {
            return !(string.IsNullOrWhiteSpace(name) || hasIncorrectChars(name));
        }

        public static bool isPasswordValid(string password)
        {
            return !(string.IsNullOrWhiteSpace(password) || password.Length < 3);
        }

        public static bool isNumberValid(double number)
        {
            return (number > 0);
        }

        private static bool hasIncorrectChars(string name)
        {
            string pattern = @"[^A-Za-zА-Яа-яёЁ]";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(name);
        }
    }
}