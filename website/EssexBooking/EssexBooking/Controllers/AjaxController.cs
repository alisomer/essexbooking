using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;

namespace EssexBooking.Controllers
{
    public class AjaxController : Controller
    {


        [HttpPost]
        public ActionResult AddExtraToBooking(int temp_id, int extra_id, int number, DateTime extra_date)
        {
            Cart cart = new Cart();
            ExtraBooking extraBooking = new ExtraBooking();
            extraBooking.extra_id = extra_id;
            extraBooking.participants = number;
            extraBooking.booked_date = extra_date;
            var query = entities.Extras.FirstOrDefault(x => x.id == extra_id);
            ViewBag.extra_name = query.name;
            decimal total_price = number * query.price;
            ViewBag.extra_total_price = total_price;
            Random r = new Random();
            int extrabooktemp_id = r.Next();
            
            
            
            //cart.bookings[temp_id].temp_extras.Add(extrabooktemp_id, extraBooking);







           //return PartialView("_ExtraBookingCartPartial", cart.bookings[temp_id].temp_extras);
            return PartialView("_ExtraBookingCartPartial");//TODO:FIX


        }


        public ActionResult AddHotelToCart(int hotel_id)
        {
            Cart cart = new Cart();
            Booking newbooking = new Booking();

            newbooking.id = Guid.NewGuid();
            newbooking.Hotel = cart.ctx.Hotels.FirstOrDefault(h => h.id == hotel_id);
            cart.ctx.Bookings.AddObject(newbooking);

            return PartialView("_CartPartial", cart);
        }
       
        public ActionResult RemoveBookingFromCart(Guid temp_id)
        {
            
            Cart cart = new Cart();
            cart.ctx.DeleteObject(cart.GetBooking(temp_id));
            cart.AddToSession();
            return PartialView("_CartPartial", cart);
        }

        public class BookingRequest
        {
            public Guid temp_id { get; set; }
            public DateTime start_date { get; set; }
            public int duration { get; set; }
            public int guests { get; set; }
            public int travel_type_id { get; set; }
        }

        [HttpPost]
        public JsonResult UpdateBooking(BookingRequest br)
        {
            Guid cusID = new Guid("5ae593b4-066d-4744-b9e0-35030455005b");
            Cart cart = new Cart();

            Booking b = cart.GetBooking(br.temp_id);
            b.guests = br.guests;
            b.duration = br.duration;
            b.start_date = br.start_date;
            b.customer_id = cusID;


            Travel travel = new Travel
            {
                id = Guid.NewGuid(),
                travel_type_id = br.travel_type_id,
                departure = br.start_date,
                arrival = br.start_date
            };

            b.Travel = travel;
            return Json(new {  });

        }

        public ActionResult SetGuests(Guid temp_id, int guests)
        {
            Cart cart = new Cart();
            cart.GetBooking(temp_id).guests = guests;
            return PartialView("_PassengerFormPartial", guests);
        }
    }
}
