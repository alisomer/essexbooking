using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;



namespace EssexBooking.Models
{
    public partial class Booking : EntityObject
    {
        private List<ExtraBooking> extras = new List<ExtraBooking>();

        public List<ExtraBooking> Extras
        {
            get { return extras; }
        }
    }
}