using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssexBooking.Models
{
    public class Validator
    {
        public String ValidateTravel(DateTime startDate, int travelType)
        {
            if (travelType == 2)
            {
                if ((int) startDate.DayOfWeek != 3 || (int) startDate.DayOfWeek != 6)
                {
                    return "error: cannot fly on days other than Wednesday or Saturday";
                }
            }

            return "";
        }

        public Boolean Success(List<String> errors)
        {
            foreach (String s in errors)
            {
                if (s != "")
                {
                    return false;
                }
            }
            return true;
        }
    }
}