using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;

namespace EssexBooking.Areas.Admin.ViewModels
{
    public class ExtraBookingViewModel
    {
        private ASPNETDBEntities db = new ASPNETDBEntities();

        public ExtraBookingViewModel(ExtraBooking eb, int? resort_id)
        {
            ExtraBooking = eb;
            ResortExtras = resort_id.HasValue ? new SelectList(db.Extras.Where(x => x.resort_id == resort_id), "id", "name") : new SelectList(ExtraBooking.Booking.Hotel.Resort.Extras, "id", "name", ExtraBooking.extra_id);
        }

        public ExtraBooking ExtraBooking { get; set; }
        public SelectList ResortExtras { get; set; }
    }
}
