using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;
using System.ComponentModel.DataAnnotations;



namespace EssexBooking.Models
{
    public partial class Booking : EntityObject
    {
        public double twoWeekDiscount = 0.9;
        public double groupDiscount = 0.95;
        public double singleRoomSuplement = 1.2;

        public int guests(){
            return single_rooms + double_rooms * 2;
        }

        // public decimal extra_total_price;
        public decimal GetHotelTotal()
        {
            //TODO: change guests to number of rooms

            double result = (double) Hotel.HotelType.price * (single_rooms * singleRoomSuplement + double_rooms *2 ) * duration;
            if (HasDiscount14day()) result *= twoWeekDiscount; //14 day stay discount
            return (decimal) result;
        }

        public bool HasDiscount14day()
        {
            return (duration == 14);
        }

        public bool HasDiscountGroup()
        {
            return (guests() > 10);
        }

        public bool HasSingleRoomSupplement()
        {
            return (single_rooms > 0);
        }

        public decimal GetTravelTotal()
        {
            return (Travel.TravelType.price * guests());//TODO: change to passangers
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
            double result = (double)(GetHotelTotal() + GetTravelTotal() + GetExtraTotal());
            if (HasDiscountGroup())
            {
                result *= groupDiscount;//group discount
            }

            return (decimal)result;

        }

        public Payment GetPayment(){
            return Payments.Single(p => p.booking_id == id);
        }

        /**
        public void updatePassangersFromRooms(){
            foreach (Passanger p in this.Travel.Passangers.ToList())
            {
                this.Travel.Passangers.Remove(p);
                //cart.ctx.DeleteObject(p);
            }

            //b.Travel.Passangers.Clear();
            for (int i = 0; i < guests(); i++)
            {
                this.Travel.Passangers.Add(new Passanger { id = Guid.NewGuid() });
            }
        }
        */
    }


    public class BookingRequest
    {
        [Required]
        public Guid booking_id { get; set; }

        [Required]
        public DateTime start_date { get; set; }

        [Required]
        [Range(1, 14, ErrorMessage = "Please select a duration for your trip")]
        public int duration { get; set; }

        [Required]
        public int single_rooms { get; set; }

        [Required]
        public int double_rooms { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Please select a travel type")]
        public int travel_type_id { get; set; }
    }

}