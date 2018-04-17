using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SenEvents
{
    class Validators
    {
        public static bool checkEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            return match.Success;
        }

        public static bool checkConfirmationPassword(string password, string confirmationPassword)
        {
            return password.Equals(confirmationPassword);
        }

        public static bool checkPasswordValide(string password)
        {
            // TODO
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return true;
        }
    }
}
