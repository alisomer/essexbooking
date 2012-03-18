using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssexBooking.Models;
using System.Web.Script.Serialization;

namespace EssexBooking.Controllers
{
    public class AjaxController : Controller
    {


        [HttpPost]
        public ActionResult AddExtraToBooking(Guid booking_id, int extra_id, int number, DateTime extra_date)
        {
            Cart cart = new Cart();
            ExtraBooking extraBooking = new ExtraBooking();
            extraBooking.extra_id = extra_id;
            extraBooking.participants = number;
            extraBooking.booked_date = extra_date;
            Extra extra = cart.ctx.Extras.FirstOrDefault(x => x.id == extra_id);
            extraBooking.Extra = extra;
            cart.GetBooking(booking_id).ExtraBookings.Add(extraBooking);
            decimal total_price = number * extra.price;
            ViewBag.extra_total_price = total_price;

            return PartialView("_ExtraBookingCartPartial", cart.GetBooking(booking_id).ExtraBookings);
        }


        public ActionResult AddHotelToCart(int hotel_id)
        {
            Cart cart = new Cart();
            Booking newbooking = new Booking
            {
                id = Guid.NewGuid(),
                Hotel = cart.ctx.Hotels.FirstOrDefault(h => h.id == hotel_id)
            };

            //init empty travel
            Travel travel = new Travel
            {
                id = Guid.NewGuid()
            };

            cart.ctx.AddToTravels(travel);
            newbooking.Travel = travel;

            cart.ctx.Bookings.AddObject(newbooking);

            return PartialView("_CartPartial", cart);
        }
       
        public ActionResult RemoveBookingFromCart(Guid booking_id)
        {
            
            Cart cart = new Cart();
            cart.ctx.DeleteObject(cart.GetBooking(booking_id));

            return PartialView("_CartPartial", cart);
        }

        public class BookingRequest
        {
            public Guid booking_id { get; set; }
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
            Boolean success = true;
            List<String> errors = new List<String>();
            Validator v = new Validator();

            errors.Add(v.ValidateTravel(br.start_date, br.travel_type_id));
            success = v.Success(errors);

            if (success)
            {
                Booking b = cart.GetBooking(br.booking_id);
                b.guests = br.guests;
                b.duration = br.duration;
                b.start_date = br.start_date;
                b.customer_id = cusID;

                /*
                Travel travel = new Travel
                {
                    id = Guid.NewGuid(),
                    travel_type_id = br.travel_type_id,
                    departure = br.start_date,
                    arrival = br.start_date
                };
                cart.ctx.AddToTravels(travel);
                b.Travel = travel;
                 * */

                //b.Travel.travel_type_id = br.travel_type_id;

                b.Travel.TravelType = cart.ctx.TravelTypes.SingleOrDefault(t => t.id == br.travel_type_id);
                b.Travel.departure = br.start_date;
                b.Travel.arrival = br.start_date;
            }
            return Json(new { success = success});
        }

        public ActionResult SetGuests(Guid booking_id, int guests)
        {

            Cart cart = new Cart();
            Booking b = cart.GetBooking(booking_id);
            
            foreach(Passanger p in b.Travel.Passangers.ToList()){
                b.Travel.Passangers.Remove(p);
                cart.ctx.DeleteObject(p);
            }

            //b.Travel.Passangers.Clear();
            for(int i=0; i<guests;i++){
                b.Travel.Passangers.Add(new Passanger { id = Guid.NewGuid() });
            }
            

            return PartialView("_PassengersFormPartial", b.Travel.Passangers);
        }

        public ActionResult SetRooms(Guid booking_id, int guests)
        {
            Cart cart = new Cart();
            cart.GetBooking(booking_id).guests = guests;
            return PartialView("_RoomsFormPartial", guests);
        }

        public ActionResult SetDoubles(Guid booking_id, int rooms, int guests)
        {
            Cart cart = new Cart();
            return PartialView("_DoublesFormPartial", guests-rooms);
        }

        [HttpPost]
        public JsonResult AddPassengers(Passanger passenger)
        {
            if (ModelState.IsValid)
            {

                Cart cart = new Cart();
                Booking booking = cart.GetBookings().SingleOrDefault(b => b.travel_id == passenger.travel_id);
                Passanger existing_passenger = booking.Travel.Passangers.SingleOrDefault(p => p.id == passenger.id);
                //existing_passenger = passenger;
                existing_passenger.first_name = passenger.first_name;
                existing_passenger.last_name = passenger.last_name;
                existing_passenger.passaport_no = passenger.passaport_no;
                return Json(new { success = true, id = existing_passenger.id });
            }
            return Json(new { success = false });
        }


    }
}
