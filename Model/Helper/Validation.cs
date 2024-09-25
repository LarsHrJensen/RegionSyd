using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegionSyd.Model.Helper
{
    public static class Validation
    {
        public static bool ValidHourMinute(string str)
        {
            // Checks whether string is in hh:mm format


            bool isValid = false;
            Regex validTime = new("^[012]\\d:[012345]\\d$");

            bool match = validTime.Match(str).Success;

            // 26:59 still matches, regardless of it being nonsense
            // check if starting hour number is 2, if so, second hour number can be no more than 3
            if (match)
            {
                if (str[0] == '2')
                {
                    int h = int.Parse(char.ToString(str[1]));
                    if (h <= 3)
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else
                {
                    isValid = true;
                }
            }
            else 
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
