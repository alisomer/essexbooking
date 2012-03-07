using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;

namespace EssexBooking.Controllers
{
    public class AjaxController : Controller
    {
        public ASPNETDBEntities entities = new ASPNETDBEntities();
        //
        // GET: /Ajax/

        public ActionResult Index()
        {
            return View();
        }

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
            cart.bookings[temp_id].temp_extras.Add(extrabooktemp_id, extraBooking);

            return PartialView("_ExtraBookingCartPartial", cart.bookings[temp_id].temp_extras);
        }


        public ActionResult AddHotelToCart(int hotel_id)
        {
            Cart cart = new Cart();
            Booking newbooking = new Booking();

            Random r = new Random();
            newbooking.temp_id = r.Next();//as a temp id to give ids to radios etc
            newbooking.Hotel = entities.Hotels.FirstOrDefault(h => h.id == hotel_id);
            newbooking.temp_extras = new Dictionary<int, ExtraBooking>();
            //cart.bookings.Add(newbooking);
            cart.bookings.Add(newbooking.temp_id, newbooking);

            return PartialView("_CartPartial", cart);
        }
        /*
        public ActionResult RemoveHotelFromCart(int hotel_id)
        {
            Cart cart = new Cart();
            //cart.bookings.Remove(cart.bookings.Where(b => b.Value.hotel_id == hotel_id).ToList().Key);
            foreach(var b in cart.bookings.Where(b => b.Value.hotel_id == hotel_id).ToList() ){
                cart.bookings.Remove(b.Key);
            }
            cart.AddToSession();
            return PartialView("_CartPartial", cart);
        }
        */

        public ActionResult RemoveBookingFromCart(int temp_id)
        {
            Cart cart = new Cart();
            cart.bookings.Remove(temp_id);
            cart.AddToSession();
            return PartialView("_CartPartial", cart);
        }

        public class BookingRequest
        {
            public int temp_id { get; set; }
            public DateTime start_date { get; set; }
            public int duration { get; set; }
            public int guests { get; set; }
            public int travel_type_id { get; set; }
        }

        [HttpPost]
        public JsonResult UpdateBooking(BookingRequest br)
        {
            Cart cart = new Cart();
  
            bool updated = false;
            if(cart.bookings.ContainsKey(br.temp_id)){
                cart.bookings[br.temp_id].guests = br.guests;
                cart.bookings[br.temp_id].duration = br.duration;
                
                Travel travel= new Travel();
                travel.TravelType = entities.TravelTypes.FirstOrDefault(tt => tt.id == br.travel_type_id);
                travel.departure = br.start_date;
                travel.arrival = br.start_date.AddDays(br.duration);
                cart.bookings[br.temp_id].Travel = travel;
                 
                updated = true;
            }
            
            //List of ExtraBooking
           

        //    cart.bookings[br.temp_id].ExtraBookings.Add();
            
            return Json(new { success = updated});
            
        }

    }
}
