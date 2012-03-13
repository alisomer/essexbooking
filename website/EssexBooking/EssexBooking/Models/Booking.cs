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
        private ASPNETDBEntities entities = new ASPNETDBEntities();
        // public decimal extra_total_price;
        public decimal GetHotelTotal()
        {
            //TODO: change guests to number of rooms
            return (Hotel.HotelType.price * guests);
        }

        public decimal GetTravelTotal()
        {
            //   return (Travel.TravelType.price * guests);
            return (0 * guests);
        }

        public decimal GetExtraTotal(Guid bookingid)
        {
            Cart cart = new Cart();
            decimal extra_price = 0;
            Booking booking = cart.GetBooking(bookingid);
            IEnumerable<ExtraBooking> extra_booking = booking.ExtraBookings;

            foreach (var extras in extra_booking)
            {
                Extra extra = entities.Extras.First(x => x.id == extras.extra_id);

                //in Euros
                extra_price = extra_price + (extra.price * extras.participants);

                //in pounds
                //TODO: convert to pounds
                extra_price = extra_price * decimal.Round((decimal)1.18966);
            }

            //calculate discounts


            return extra_price;
        }  
    }
}