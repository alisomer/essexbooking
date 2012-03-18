using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using EssexBooking.Models;

namespace EssexBooking.Areas.Admin.ViewModels
{
    public class BookingsViewModel
    {
        public BookingsViewModel(IEnumerable<Booking> bookings)
        {
            Bookings = bookings;
            ExtraBookings = new Dictionary<Guid, IEnumerable<ExtraBooking>>();
            foreach (var booking in Bookings)
            {
                ExtraBookings.Add(booking.id, booking.ExtraBookings);
            }
        }
        public IEnumerable<Booking> Bookings { get; set; }
        public Dictionary<Guid, IEnumerable<ExtraBooking>> ExtraBookings { get; set; }
    }
}
