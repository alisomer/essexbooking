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

        // public decimal extra_total_price;
        public decimal GetHotelTotal()
        {
            //TODO: change guests to number of rooms
            return (Hotel.HotelType.price * guests);
        }

        public decimal GetTravelTotal()
        {
            return (Travel.TravelType.price * guests);//TODO: change to passangers
        }

        public decimal GetExtraTotal()
        {
            decimal extra_price = 0;
            IEnumerable<ExtraBooking> extra_booking = this.ExtraBookings;

            foreach (var extra in extra_booking)
            {
                //in Euros
                extra_price = extra_price + (extra.Extra.price * extra.participants);
            }

            //TODO:calculate discounts

            return (decimal)Currency.Exchange("EUR", "GBP", (double)extra_price);
        }

        public decimal GetBookingTotal()
        {
            return GetHotelTotal() + GetTravelTotal() + GetExtraTotal();

        }

    }
}