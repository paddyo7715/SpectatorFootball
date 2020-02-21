using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpectatorFootball.Common
{
    class Validator
    {
        public static bool isValidateColorString(string c)
        {
            Boolean r = true;

            if (c == null)
                r = false;
            else if (c.Length != 7)
                r = false;
            else if (!c.StartsWith("#"))
                r = false;
            else
            {
                Regex myRegex = new Regex("^[a-fA-F0-9]+$");
                r = myRegex.IsMatch(c.Substring(1));
            }


            return r;
        }


    }
}
